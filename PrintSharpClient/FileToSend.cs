using System;

namespace PrintSharpClient
{
    internal class FileToSend
    {
        private readonly string _name;
        private readonly int _size;

        public FileToSend(String name, int size)
        {
            _name = name;
            _size = size;
        }

        public string JsonData()
        {
            return "{\"name\" : \"" + _name + "\", \"size\" : \"" + _size + "\"}";
        }
    }
}