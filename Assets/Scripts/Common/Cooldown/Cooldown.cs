using System;

namespace Common
{
	public class Cooldown
	{
		public event Action<Cooldown> Completed;
		public event Action<Cooldown> Updated;

		public DateTime StartDate { get; set; }

		private DateTime _completionDate;

		public DateTime CompletionDate
		{
			get => _completionDate;
			set { _completionDate = value; }
		}

		public string Id { get; }

		public bool IsComplete => DateTime.Now > CompletionDate;

		public Cooldown(string id, DateTime completionDate)
		{
			Id = id;
			CompletionDate = completionDate;
		}

		public void OnUpdated()
		{
			Updated?.Invoke(this);
		}

		public void OnCompleted()
		{
			Completed?.Invoke(this);
		}
	}
}