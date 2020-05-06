using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HPADesign.Models;

namespace HPADesign.Utilities
{
    /// <summary>
    /// TODO
    /// </summary>
    interface IAirfoilCoordinateDetect
    {
        AirfoilCoordinate Detect();
        void Close();
    }
    interface IAirfoilRead
    {
        Airfoil Read();
        void Close();
    }
    interface AirfoilWrite
    {
        
    }
    public class CoordinateDetector : IAirfoilCoordinateDetect
    {
        StreamReader sr;
        string file;
        public CoordinateDetector(string file)
        {
            this.file = file;
        }
        public CoordinateDetector(Stream stream)
        {
            sr = new StreamReader(stream);
            file = sr.ReadToEnd();
        }
        public AirfoilCoordinate Detect()
        {
            string[] datstring = file.Split(' ');
            for (int i = 0; i < datstring.Length; i++)
            {
                double x = -1;
                if (datstring[i].Equals(""))
                {
                    continue;
                }
                //TODO
                x = double.Parse(datstring[i]);
                if (x > 0.1)
                {
                    //Seligパターン
                    var selig = new SeligCoordinate();

                    return selig;
                }
                else
                {
                    //Lednicerパターン
                    var lednicer = new LednicerCoordinate();

                    return lednicer;
                }
            }
            return new NullCoordinate();
        }
        public void Close()
        {
            if(sr!=null)
                sr.Dispose();
        }
    }
    public class AirfoilReader : IAirfoilRead
    {
        private string name;
        private string file;
        private string path;
        //private Stream stream;
        StreamReader sr;
        public AirfoilReader(string path)
        {
            this.path = path;
            sr = new StreamReader(path);
            this.name = sr.ReadLine();
            this.file = sr.ReadToEnd();
        }
        public AirfoilReader(Stream stream,string name)
        {
            
            //stream.CopyTo(this.stream);
            sr = new StreamReader(stream);
            //file = sr.ReadToEnd();
            this.name = sr.ReadLine();
            this.file = sr.ReadToEnd();
        }

        public Airfoil Read()
        {
            var detector = new CoordinateDetector(file);
            var result = new Airfoil();
            result.Name.Value = name;
            result.Coordinate = detector.Detect();
            detector.Close();

            string[] datstring = file.Split(' ');
            //いったん格納して後から二つずつPosにぶち込む
            List<double> posraw = new List<double>();
            for (int i = 0; i < datstring.Length; i++)
            {
                double x;
                if (double.TryParse(datstring[i], out x))
                {
                    posraw.Add(x);
                }
            }
            //偶数個ならぶち込む
            if (posraw.Count % 2 == 1)
            {
                //エラーとして空のAirfoilインスタンスを投げる
                return null;
            }
            List<Pos> Coordinate = new List<Pos>();
            for (int i = 0; i < posraw.Count / 2; i++)
            {
                Coordinate.Add(new Pos(posraw[2*i], posraw[2*i + 1]));
            }
            result.Coordinate.Points = Coordinate;
            return result;
        }
        public void Close()
        {
            if(sr!=null)
                sr.Dispose();
        }
    }
}
