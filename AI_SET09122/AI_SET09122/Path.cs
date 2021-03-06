﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_SET09122
{
    class Path
    {
        public Node getPath(Node startNode, Node finalNode)
        {
             Node thisNode = startNode;
             thisNode.G = distanceBetweenNodes(startNode, thisNode);
             thisNode.H = distanceBetweenNodes(thisNode, finalNode);
             thisNode.calcF();
             return thisNode;
        }

        double distanceBetweenNodes(Node CurrentNode, Node NextNode)
        {
            Node thisNode = CurrentNode;
            Node targetNode = NextNode;
            double Distance;

            Distance = Math.Sqrt(((targetNode.X_coord = thisNode.X_coord) * (targetNode.X_coord - thisNode.X_coord)) + ((targetNode.Y_coord - thisNode.Y_coord) * (targetNode.Y_coord - thisNode.Y_coord)));
            return Distance;
        }

        List<Node> caveRouteLists(int[,] caveRoutes, List<Node> nodeList, Node startNode, int noCaves, Node finalNode)
        {
            List<Node> routeList = new List<Node>();
            Node thisNode = new Node();

            for (int i = 0; i < noCaves; i++)
            {
                if (caveRoutes[i, startNode.caveid - 1] == 1)
                {
                    thisNode = nodeList.Find(x => x.caveid == i + 1);
                    if (thisNode.state == NodeState.Closed || thisNode == null)
                    {
                        continue;
                    }
                    thisNode.parentNode = startNode;
                    thisNode.state = NodeState.Open;
                    getPath(startNode, thisNode);
                    routeList.Add(thisNode);
                }
            }
            routeList.Sort((x, y) => x.F.CompareTo(y.F));
            return routeList;
        }

        public bool nodeSearch(Node thisNode, int[,] caveRoutes, List<Node> nodeList, int cavesNo, Node finalNode)
        {
            List<Node> searchList = caveRouteLists(caveRoutes, nodeList, thisNode, cavesNo, finalNode);
            thisNode.state = NodeState.Closed;

            foreach(var nextNode in searchList)
            {
                if(nextNode == finalNode)
                {
                    return true;
                }
                else
                {
                    if(nodeSearch(nextNode, caveRoutes, nodeList, cavesNo, finalNode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Node> FoundPath(int[,] caveRoutes, List<Node> nodeList, Node startNode, int cavesNo, Node finalNode)
        {
            List<Node> thisPath = new List<Node>();
            bool success = nodeSearch(startNode, caveRoutes, nodeList, cavesNo, finalNode);
            if (success)
            {
                Node thisNode = finalNode;
                while(thisNode.parentNode != null)
                {
                    thisPath.Add(thisNode);
                    thisNode = thisNode.parentNode;
                }
                thisPath.Reverse();
            }
            return thisPath;
        }
    }
}
