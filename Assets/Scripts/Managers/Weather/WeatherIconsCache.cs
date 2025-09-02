using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Managers
{
	public class WeatherIconsCache
	{
		private WeatherNode _root;

		public WeatherIconsCache()
		{
			_root = new WeatherNode("root");
		}

		public void ParseUrl(string url, out WeatherNode node)
		{
			Uri uriResult = new Uri(url);
			var tokens = uriResult.LocalPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
			var slice = tokens[2..];

			node = _root;

			foreach (var token in slice)
			{
				TryGetNode(node, token, out node);
			}
		}

		private void TryGetNode(WeatherNode root, string token, out WeatherNode node)
		{
			if (root.CheckWeatherNode(token, out node))
				return;

			node = new WeatherNode(token);
			root.AddNode(node);
		}

		public class WeatherNode
		{
			private string _name;
			public string Name => _name;

			private Sprite _spriteIcon;
			public Sprite SpriteIcon => _spriteIcon;

			public WeatherNode(string name)
			{
				_name = name;
			}

			Lazy<List<WeatherNode>> children = new(() => new List<WeatherNode>());

			public bool CheckWeatherNode(string name, out WeatherNode node)
			{
				node = null;

				foreach (var child in children.Value)
				{
					if (child._name.Equals(name))
					{
						node = child;
						return true;
					}
				}

				return false;
			}

			public void SetSprite(Texture2D tex)
			{
				var sprite = CreateSpriteFromTexture(tex);
				_spriteIcon = sprite;
			}

			private Sprite CreateSpriteFromTexture(Texture2D texture)
			{
				Sprite newSprite = Sprite.Create(texture,
					new Rect(0, 0, texture.width, texture.height),
					new Vector2(0f, 0f));
				return newSprite;
			}

			public void AddNode(WeatherNode node)
			{
				children.Value.Add(node);
			}

			public void Clear()
			{
				children.Value.Clear();
			}
		}
	}
}