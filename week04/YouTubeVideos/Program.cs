using System;
using System.Collections.Generic;

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


    /// Represents a YouTube video with its metadata and associated comments.
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }
    }

    
    /// Main program that creates videos, adds comments, and displays the information.
       class Program
    {
        static void Main(string[] args)
        {
            // Create a list to hold all videos
            List<Video> videos = new List<Video>();

            // ------------------- Video 1 -------------------
            Video video1 = new Video("C# Abstraction Explained", "Ronald", 450);
            video1.AddComment(new Comment("Alice", "Great explanation, very clear!"));
            video1.AddComment(new Comment("Bob", "I finally understand abstraction, thanks!"));
            video1.AddComment(new Comment("Charlie", "Could you make a video on inheritance next?"));
            videos.Add(video1);

            // ------------------- Video 2 -------------------
            Video video2 = new Video("Top 10 Programming Tips", "Joaquin", 500);
            video2.AddComment(new Comment("Diana", "Tip #3 changed the way I code."));
            video2.AddComment(new Comment("Eve", "Very useful, saved to my playlist."));
            video2.AddComment(new Comment("Frank", "I would add 'always use version control'."));
            video2.AddComment(new Comment("Grace", "Thanks for sharing these insights!"));
            videos.Add(video2);

            // ------------------- Video 3 -------------------
            Video video3 = new Video("OOP in 15 Minutes", "John", 900);
            video3.AddComment(new Comment("Henry", "Quick and to the point, loved it!"));
            video3.AddComment(new Comment("Ivy", "This helped me prepare for my interview."));
            video3.AddComment(new Comment("Jack", "Subscribed for more OOP content."));
            videos.Add(video3);

            // ------------------- Video 4 -------------------
            Video video4 = new Video("Understanding YouTube Analytics", "Kabanda", 1140);
            video4.AddComment(new Comment("Kevin", "Finally someone explains retention graphs."));
            video4.AddComment(new Comment("Laura", "Can you do a follow-up on SEO?"));
            video4.AddComment(new Comment("Mia", "Very detailed, exactly what I needed."));
            videos.Add(video4);

            // Display all videos and their comments
            foreach (Video video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"  - {comment.CommenterName}: \"{comment.Text}\"");
                }

                Console.WriteLine(); // Blank line between videos
            }
        }
    }
}