using System;

namespace YouTubeVideos
{
    /// <summary>
    /// Represents a comment left on a YouTube video.
    /// </summary>
    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }
}
