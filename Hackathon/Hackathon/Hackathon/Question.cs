using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Hackathon
{
    class Question
    {
        public Texture2D question { get; set; }
        public Texture2D left_answer { get; set; }
        public LinkedList<Texture2D> left_duds { get; set; }
        public Texture2D right_answer { get; set; }

        public Question(String directory, ContentManager content)
        {
            this.question = content.Load<Texture2D>(directory + "/question");
            this.left_answer = content.Load<Texture2D>(directory + "/left_answer");
            this.left_duds = new LinkedList<Texture2D>();
            for (int i = 0; i < 3; i++)
            {
                this.left_answers.AddLast(content.Load<Texture2D>(directory + "/left_answer_" + i));
            }
            this.right_answer = content.Load<Texture2D>(directory + "/right_answer");
        }
    }
}
