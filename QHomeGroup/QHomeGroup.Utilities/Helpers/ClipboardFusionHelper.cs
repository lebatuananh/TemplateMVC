using System;
using System.Text.RegularExpressions;

namespace QHomeGroup.Utilities.Helpers
{
	public static class ClipboardFusionHelper
	{
		public static string ProcessText(string url)
		{
			if (url.Contains("youtube.com") || url.Contains("youtu.be"))
			{
				return ClipboardFusionHelper.ConvertYouTubeToEmbed(url);
			}

			if (url.Contains("vimeo.com"))
			{
				return ClipboardFusionHelper.ConvertVimeoToEmbed(url);
			}

			return url;
		}

		private static string ConvertYouTubeToEmbed(string url)
		{
			string[] parts = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			string idWithPotentialQueryParams = parts[parts.Length - 1].ToString().Replace("?t=", "?start=");
			string joiner = idWithPotentialQueryParams.Contains("?") ? "&" : "?";
			string suffix = "ecver=2";
			return "https://www.youtube.com/embed/" + idWithPotentialQueryParams + joiner + suffix;
		}

		private static string ConvertVimeoToEmbed(string url)
		{
			string[] parts = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			string id = parts[parts.Length - 1].ToString();
			return "https://player.vimeo.com/video/" + id;
		}

		public static string YoutubeUrlConverter(string url)
		{
			var YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
			Match youtubeMatch = YoutubeVideoRegex.Match(url);
			return youtubeMatch.Success ? "https://www.youtube.com/embed/" + youtubeMatch.Groups[1].Value : string.Empty;
		}

		public static string GetEmbededId(string url)
		{
			return Regex.Match(url, @"(?:youtube(?:-nocookie)?\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})").Groups[1].Value;
		}
	}
}