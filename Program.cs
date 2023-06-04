using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Graphs_Acycl_new
{
    class Vertex
    {
        public int Out;
        public int To;

        public override string ToString()
        {
            return $"Out: {Out} To: {To} ";
        }

        public Vertex(int Out, int To)
        {
            this.Out = Out; this.To = To;
        }
    }
    class Program
    {
        //static List<Vertex> Init3(List<Vertex> toFill)
        //{
        //    toFill = new List<Vertex>()
        //    {
        //        new Vertex(1, new List<int>() { 3,4 })
        //        ,new Vertex(2, new List<int>() { 1,5})
        //        ,new Vertex(3, new List<int>() { 2 })
        //        ,new Vertex(4, new List<int>() { 5 })
        //        ,new Vertex(5, new List<int>() { 3 })
        //    };
        //    return toFill;
        //}
        //static List<Vertex> Init2(List<Vertex> toFill)
        //{
        //    toFill = new List<Vertex>()
        //    {
        //        new Vertex(1, new List<int>() { 2 })
        //        ,new Vertex(2, new List<int>() { 3})
        //        ,new Vertex(3, new List<int>() { 1,4,5 })
        //        ,new Vertex(4, new List<int>() { 1 })
        //        ,new Vertex(5, new List<int>() { 4 })
        //    };
        //    return toFill;
        //    //1-2, 2-3, 3-4, 3-5, 5-4
        //}
        static List<Vertex> Init1(List<Vertex> toFill)
        {
            toFill = new List<Vertex>()
            {
                 new Vertex(1,4)
                ,new Vertex(1,3)
                ,new Vertex(2,1)
                ,new Vertex(2,5)
                ,new Vertex(3,2)
                ,new Vertex(4,5)
                ,new Vertex(5,3)
                //Номера записывать обязательно с 1, не пропуская цифр, исходящие вершины в порядке возрастания
                //Ответ: 1,2,3,4,5,6,7
            };
            return toFill;
        }
        static List<Vertex> Ans = new List<Vertex>();

        static bool Deep_Check(List<Vertex> ToCheck, int Out, List<int> visitedPoints)
        {
            var newvisited = new List<int>(visitedPoints);
            newvisited.Add(Out);
            var togo = new List<int>();
            foreach(var ver in ToCheck)
            {
                if(ver.Out == Out) {togo.Add(ver.To);}
            }
            //нашли все исходящие ребра из вершины
            
            if(togo.Count == 0) //если конечная
            {
                return true;
            }
            var tech = true;
            foreach(var ver in togo)
            {
                if(newvisited.Contains(ver))
                {
                    return false; //если из вершины идет ребро в уже прошедшие вершины
                }

                var bolch = Deep_Check(ToCheck, ver, newvisited);
                if(!bolch)
                {
                    tech = false;
                    break;
                }
            }
            return tech;

        }
        static bool Check_Acycl(List<Vertex> ToCheck)
        {
            //вернуть true если нет цикла, false при наличии
            var visitedPoints = new List<int>();
            var bolch = true;
            for(int i = 0; i < ToCheck.Count; i++)
            {
                var visitedpoints = new List<int>();
                visitedpoints.Add(ToCheck[i].Out);
                var tech = Deep_Check(ToCheck, ToCheck[i].To, visitedpoints);
                if (!tech)
                {
                    bolch = false; // найден цикл
                    break;
                }
            }
            return bolch; // если не пришло вообще ни единой вершины
        }

        static void Check_Is_Ans(List<Vertex> ToCheck)
        {
            bool Acycl = Check_Acycl(ToCheck);
            if (Acycl)
            {
                if((Ans.Count == 0) && (ToCheck.Count != 0))
                {
                    Ans = new List<Vertex>(ToCheck);
                }
                else if(Ans.Count < ToCheck.Count)
                {
                    Ans = new List<Vertex>(ToCheck);
                }
            }
        }

        static void Nest_Shuffle(ref List<Vertex> conPoint, List<Vertex> PrevPoint, int v)
        {
            Check_Is_Ans(PrevPoint);
            int n = v + 1;
            for(int i = n; i < conPoint.Count; i++)
            {
                var temppoints = new List<Vertex>(PrevPoint) { conPoint[i] };
                Nest_Shuffle(ref conPoint, temppoints, i);
            }

        }
        static void Shuffle(List<Vertex> points)
        {
            for(int i = 0; i < points.Count; i++)
            {
                var tempoints = new List<Vertex>() { points[i] };
                Nest_Shuffle(ref points, tempoints, i);
            }
        }
             
        static void Main()
        {
            var Points = new List<Vertex>();
            Points = Init1(Points);
            //var Ans = new List<string>();
            Shuffle(Points);
            foreach (var t in Ans)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}