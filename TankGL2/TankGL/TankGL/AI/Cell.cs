using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankGL.AI
{
    class Cell
    {
        String type;

        public Cell(String type)
        {
            this.type = type; 
        }

        public void setType(String t)
        {
            this.type = t;
        }

        public String getType()
        {
            return this.type;
        }

    }
}
