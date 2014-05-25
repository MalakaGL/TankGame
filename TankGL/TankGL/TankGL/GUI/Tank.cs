using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGL.AI;

namespace TankGL.GUI
{
    class Tank
    {
        int direcion, x = 0 , y = 0;
        int ID;

        public Tank(int id, int x, int y, int d)
        {
            this.x = x;
            this.y = y;
            this.direcion = d;
            this.ID = id;
        }

        public int getId()
        {
            return ID;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getDirection()
        {
            return direcion;
        }
    }
}
