using System.Collections.Generic;

namespace VideoDownloader.Core
{
    public class Extentisons
    {
        private static readonly IDictionary<int, string> Instances;

        public int Id { get; private set; }

        public string Value { get; private set; }

        static Extentisons()
        {
            Instances = new Dictionary<int, string>
            {
                {5, "flv"},
                {6, "flv"},
                {17, "3gp"},
                {18, "mp4"},
                {22, "mp4"},
                {34, "flv"},
                {35,"flv"},
                {36,"3gp"},
                {37, "mp4"},
                {38, "mp4"},
                {43,"webm"},
                {44,"webm"},
                {45,"webm"},
                {46,"webm"},
                {82,"3D.mp4"},
                {83,"3D.mp4"},
                {84,"3D.mp4"},
                {85,"3D.mp4"},
                {100,"3D.webm"},
                {101,"3D.webm"},
                {102,"3D.webm"},
                {120,"live.flv"}
            };
        }

        public Extentisons(int id)
        {
            Id = id;
            Value = Instances[id];
        }

        public override string ToString()
        {
            return Value;
        }
    }
}