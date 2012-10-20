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
        private Texture2D question;
        private LinkedList<Texture2D> left_answers;
        private Texture2D right_answer;

        public Question(String directory, ContentManager content)
        {
            this.question = content.Load<Texture2D>(directory + "/question");
            this.left_answers = new LinkedList<Texture2D>();
            for (int i = 0; i < 4; i++)
            {
                this.left_answers.AddLast(content.Load<Texture2D>(directory + "/left_answer_" + i));
            }
            this.right_answer = content.Load<Texture2D>(directory + "/right_answer");
        }
    }
}
