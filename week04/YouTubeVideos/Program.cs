using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
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

            //Title
            Console.WriteLine("===== YOUTUBE VIDEOS PROGRAM=====");

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