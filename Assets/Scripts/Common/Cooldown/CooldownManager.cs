using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Common
{
	public class CooldownManager
	{
		int _checkIntervalMs = 200;

		public static readonly DateTime DataConst = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private readonly Dictionary<string, Cooldown> _cooldowns = new Dictionary<string, Cooldown>();

		private Dictionary<string, CancellationTokenSource> _cooldownTasks =
			new Dictionary<string, CancellationTokenSource>();

		public Cooldown SetCooldown(string id, TimeSpan delay)
		{
			var endTime = DateTime.Now.Add(delay);
			return SetCooldown(id, endTime);
		}

		public Cooldown SetCooldown(string id, DateTime time)
		{
			var cooldown = GetCooldown(id);
			cooldown.CompletionDate = time;
			TryRegisterCooldown(cooldown);
			return cooldown;
		}

		public Cooldown GetCooldown(string id, bool createIfNotExist = true)
		{
			if (!HasCooldown(id))
				return createIfNotExist ? CreateCooldown(id) : null;

			return _cooldowns[id];
		}

		private bool HasCooldown(string id)
		{
			return _cooldowns.ContainsKey(id);
		}

		private Cooldown CreateCooldown(string id)
		{
			var cooldown = new Cooldown(id, DataConst);
			_cooldowns.Add(id, cooldown);

			return cooldown;
		}

		private void TryRegisterCooldown(Cooldown cooldown)
		{
			if (cooldown.IsComplete)
				return;

			if (IsAlreadyRegistered(cooldown.Id))
				return;
			RegisterCooldown(cooldown);
		}

		private bool IsAlreadyRegistered(string cooldownId)
		{
			return _cooldownTasks.ContainsKey(cooldownId);
		}

		private void RegisterCooldown(Cooldown cooldown)
		{
			var cancellation = new CancellationTokenSource();
			_cooldownTasks.Add(cooldown.Id, cancellation);

			_ = StartCooldown(cooldown, cancellation.Token);
		}

		private async UniTask StartCooldown(Cooldown cooldown, CancellationToken cancelToken)
		{
			while (!cooldown.IsComplete)
			{
				await UniTask.Delay(_checkIntervalMs, cancellationToken: cancelToken);

				if (cancelToken.IsCancellationRequested)
					return;

				cooldown.OnUpdated();
			}

			if (cancelToken.IsCancellationRequested)
				return;

			_cooldownTasks[cooldown.Id].Dispose();
			_cooldownTasks.Remove(cooldown.Id);

			try
			{
				cooldown.OnCompleted();
			}
			catch (Exception ex)
			{
				Debug.LogError($"Cooldown {cooldown.Id} completion handler error!\n{ex.Message}");
			}
		}

		public void RemoveCooldown(string id)
		{
			_cooldowns.Remove(id);

			if (!_cooldownTasks.ContainsKey(id))
				return;

			_cooldownTasks[id].Cancel();
			_cooldownTasks[id].Dispose();
			_cooldownTasks.Remove(id);
		}
	}
}