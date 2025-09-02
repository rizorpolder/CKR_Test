using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class WeatherIconsCache
	{
		private readonly WeatherNode _root = new("root");

		public void ParseUrl(string url, out WeatherNode node)
		{
			var uriResult = new Uri(url);
			var tokens = uriResult.LocalPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
			var slice = tokens[2..];

			node = _root;

			foreach (var token in slice) TryGetNode(node, token, out node);
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
			private readonly Lazy<List<WeatherNode>> children = new(() => new List<WeatherNode>());

			public WeatherNode(string name)
			{
				Name = name;
			}

			public string Name { get; }

			public Sprite SpriteIcon { get; private set; }

			public bool CheckWeatherNode(string name, out WeatherNode node)
			{
				node = null;

				foreach (var child in children.Value)
					if (child.Name.Equals(name))
					{
						node = child;
						return true;
					}

				return false;
			}

			public void SetSprite(Texture2D tex)
			{
				var sprite = CreateSpriteFromTexture(tex);
				SpriteIcon = sprite;
			}

			private Sprite CreateSpriteFromTexture(Texture2D texture)
			{
				var newSprite = Sprite.Create(texture,
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