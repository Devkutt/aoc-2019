using System;
using System.IO;
using System.Linq;

namespace devkutt
{
    class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Line
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public char Direction()
        {
            if (Start.X < End.X)
            {
                return 'R';
            }
            if (Start.X > End.X)
            {
                return 'L';
            }
            if (Start.Y < End.Y)
            {
                return 'U';
            }
            return 'D';
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FileStream fileStream = new FileStream("input.txt", FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                var line1 = reader.ReadLine();
                var line2 = reader.ReadLine();
                var path1 = line1.Split(",");
                var path2 = line2.Split(",");
                var lines1 = GetLines(path1);
                var lines2 = GetLines(path2);

                int bestLength = int.MaxValue;

                for (int i = 0; i < lines1.Length; i++)
                {
                    var lineA = lines1[i];
                    for (int j = 0; j < lines2.Length; j++)
                    {
                        var lineB = lines2[j];
                        var crossPoint = CrossPoint(lineA, lineB);
                        if (crossPoint != null)
                        {
                            var steps1 = path1.Take(i).ToArray();
                            var steps2 = path2.Take(j).ToArray();

                            var length1 = LineLength(steps1) + PointDistance(lineA.Start, crossPoint);
                            var length2 = LineLength(steps2) + PointDistance(lineB.Start, crossPoint);

                            Console.WriteLine(length1 + " " + length2);

                            var lengthSum = length1 + length2;
                            if (lengthSum < bestLength)
                            {
                                bestLength = lengthSum;
                            }
                        }
                    }
                }

                Console.WriteLine(bestLength);
            }
        }

        static int LineLength(string[] steps)
        {
            int length = 0;
            foreach (var step in steps)
            {
                var stepLength = int.Parse(step.Substring(1));
                length += stepLength;
            }

            return length;
        }

        static int PointDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        static Point CrossPoint(Line line1, Line line2)
        {
            var dir1 = line1.Direction();
            var dir2 = line2.Direction();
            if (dir1 == 'R' || dir1 == 'L')
            {
                if (dir2 == 'R' || dir2 == 'L')
                {
                    return null;
                }

                var possibleX = line2.Start.X;
                if (!(possibleX < line1.Start.X && possibleX > line1.End.X || possibleX > line1.Start.X && possibleX < line1.End.X))
                {
                    // x is not on line 1
                    return null;
                }
                var possibleY = line1.Start.Y;
                if (!(possibleY < line2.Start.Y && possibleY > line2.End.Y || possibleY > line2.Start.Y && possibleY < line2.End.Y))
                {
                    // y is not on line 2
                    return null;
                }
                return new Point(possibleX, possibleY);
            }
            if (dir1 == 'U' || dir1 == 'D')
            {
                if (dir2 == 'U' || dir2 == 'D')
                {
                    return null;
                }

                var possibleX = line1.Start.X;
                if (!(possibleX < line2.Start.X && possibleX > line2.End.X || possibleX > line2.Start.X && possibleX < line2.End.X))
                {
                    // x is not on line 2
                    return null;
                }
                var possibleY = line2.Start.Y;
                if (!(possibleY < line1.Start.Y && possibleY > line1.End.Y || possibleY > line1.Start.Y && possibleY < line1.End.Y))
                {
                    // y is not on line 1
                    return null;
                }
                return new Point(possibleX, possibleY);
            }
            return null;
        }

        static Line[] GetLines(string[] path)
        {
            Line[] lines = new Line[path.Length];
            Point lastPosition = new Point(0, 0);
            int i = 0;
            foreach (var entry in path)
            {
                var direction = entry[0];
                var value = entry.Substring(1);
                switch (direction)
                {
                    case 'R':
                        lines[i] = new Line(lastPosition, new Point(lastPosition.X + int.Parse(value), lastPosition.Y));
                        break;
                    case 'U':
                        lines[i] = new Line(lastPosition, new Point(lastPosition.X, lastPosition.Y + int.Parse(value)));
                        break;
                    case 'L':
                        lines[i] = new Line(lastPosition, new Point(lastPosition.X - int.Parse(value), lastPosition.Y));
                        break;
                    case 'D':
                        lines[i] = new Line(lastPosition, new Point(lastPosition.X, lastPosition.Y - int.Parse(value)));
                        break;
                }
                lastPosition = lines[i].End;
                i++;
            }

            return lines;
        }
    }
}
