using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Runtime.InteropServices;

using System.Runtime.Intrinsics.X86;

using System.Security.AccessControl;

using System.Threading.Tasks;

using System.Transactions;
using System.Security.Cryptography;
using static GetLocation.Class.Trilateration;

namespace GetLocation.Class
{
    public class Trilateration
    {
        private const double xKenobi = -500;
        private const double yKenobi = -200;
        private const double xSkywalker = 100;
        private const double ySkywalker = -100;
        private const double xSato = 500;
        private const double ySato = 100;


      
        public static point Compute( double r1, double r2, double r3)
        {
            point resultPose = new point();

            
            double p2p1Distance = Math.Pow(Math.Pow(xSkywalker - xKenobi, 2) + Math.Pow(ySkywalker - yKenobi, 2), 0.5);
            point ex = new point() {x= (xSkywalker - xKenobi) / p2p1Distance,
                                    y= (ySkywalker - yKenobi) / p2p1Distance };
            

            point aux = new point(){ x=(xSato - xKenobi),
                                     y=(ySato - yKenobi) };

            
            double i = ex.x * aux.x + ex.y * aux.y;
            
            point aux2 = new point(){ x= (xSato - xKenobi - i * ex.x), 
                                      y= (ySato - yKenobi - i * ex.y) };


            Trilateration tr = new Trilateration();

            point ey = new point() { x= (aux2.x / tr.norm(aux2)), 
                                     y= (aux2.y / tr.norm(aux2)) };

            
            double j = ey.x * aux.x + ey.y * aux.y;
            
            double x = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(p2p1Distance, 2)) / (2 * p2p1Distance);
            double y = (Math.Pow(r1, 2) - Math.Pow(r3, 2) + Math.Pow(i, 2) + Math.Pow(j, 2)) / (2 * j) - i * x / j;
          
            double finalX = xKenobi + x * ex.x + y * ey.x;
            double finalY = yKenobi + x * ex.y + y * ey.y;

            resultPose.x = finalX;
            resultPose.y = finalY;
            return resultPose;
        }


        public struct point
        {
            public double x { get; set; } 
            public double y { get; set; }
        }

        public double norm(point p)
        {
            return Math.Pow(Math.Pow(p.x, 2) + Math.Pow(p.y, 2), .5);
        }

    }
}
