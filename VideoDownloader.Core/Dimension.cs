using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VideoDownloader.Core
{
    public class Dimension
    {
        private static readonly IDictionary<int, Size> Instances;

        static Dimension()
        {
            Instances = new Dictionary<int, Size>
            {
                {5, new Size(320,180)},
                {6, new Size(480,270)},
                {17,new Size(176,99)},
                {18,new Size(640,360)},
                {22,new Size(1280,720)},
                {34,new Size(640,360)},
                {35,new Size(854,480)},
                {36,new Size(320,180)},
                {37,new Size(1920,1080)},
                {38,new Size(2048,1152)},
                {43,new Size(640,360)},
                {44,new Size(854,480)},
                {45,new Size(1280,720)},
                {46,new Size(1920,1080)},
                {82,new Size(480,270)},
                {83,new Size(640,360)},
                {84,new Size(1280,720)},
                {85,new Size(1920,1080)},
                {100,new Size(640,360)},
                {101,new Size(640,360)},
                {102,new Size(1280,720)},
                {120,new Size(1280,720)}
            };
        }

        public int Id { get; private set; }

        private Size _size;

        public int Width
        {
            get { return _size.Width; }
        }

        public int Height
        {
            get { return _size.Height; }
        }

        public Dimension(int id)
        {
            Id = id;
            _size = Instances[id];
        }

        public override string ToString()
        {
            var s = _size.Height >= 720 ? " HD" : "";
            return _size.Width + " x " + _size.Height + s;
        }
    }
}