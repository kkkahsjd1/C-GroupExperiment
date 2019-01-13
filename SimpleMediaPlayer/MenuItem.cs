﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediaPlayer
{
    public class MenuItem
    {
        private Image img;
        private String text;
        public Image Img { get { return img; } set { img = value; } }
        public string Text { get { return text; } set{ text = value; } }

        public MenuItem(Image img, string text)
        {
            this.img = img;
            this.text = text;
        }
    }
}
