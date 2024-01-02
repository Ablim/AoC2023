namespace Solutions.Day25
{
    public static class Solution
    {
        public static int Day => 25;
        
        public static string SolvePart1(string[] rows)
        {
            var groups = rows.ParseGraph()
                .ExhaustiveSplit()
                .GetGroupSizes();
            return (groups.left * groups.right).ToString();
        }

        private static Dictionary<string, HashSet<string>> ParseGraph(this string[] rows)
        {
            var parts = rows
                .Select(row => row.Split(':'))
                .ToArray();
            var lookup = new Dictionary<string, HashSet<string>>();

            foreach (var row in parts)
            {
                var left = row[0];
                var right = row[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (!lookup.ContainsKey(left))
                    lookup.Add(left, new HashSet<string>());

                foreach (var r in right)
                    lookup[left].Add(r);
                
                foreach (var r in right)
                {
                    if (!lookup.ContainsKey(r))
                        lookup.Add(r, new HashSet<string>());

                    lookup[r].Add(left);
                }
            }
            
            return lookup;
        }

        private static bool IsConnected(this Dictionary<string, HashSet<string>> graph)
        {
            var nodes = graph.Keys.Count;
            var visited = new HashSet<string>();
            var nodesToVisit = new List<string>
            {
                graph.Keys.First()
            };

            while (nodesToVisit.Any())
            {
                var current = nodesToVisit.First();
                visited.Add(current);
                nodesToVisit.Remove(current);

                foreach (var adjacent in graph[current])
                {
                    if (!visited.Contains(adjacent))
                        nodesToVisit.Add(adjacent);
                }
            }
            
            return visited.Count == nodes;
        }

        private static Dictionary<string, HashSet<string>> ExhaustiveSplit(this Dictionary<string, HashSet<string>> graph)
        {
            var edges = new HashSet<Edge>();
            
            foreach (var nodeA in graph.Keys)
            {
                foreach (var nodeB in graph[nodeA])
                {
                    if (!edges.Contains(new Edge(nodeB, nodeA)))
                        edges.Add(new Edge(nodeA, nodeB));
                }
            }

            var combinations = EdgeCombinations(edges.ToList(), new List<Edge>(), new List<List<Edge>>());

            foreach (var combo in combinations)
            {
                foreach (var c in combo)
                {
                    graph[c.NodeA].Remove(c.NodeB);
                    graph[c.NodeB].Remove(c.NodeA);
                }

                if (!graph.IsConnected() && graph.GetGroupSizes() is { left: > 1, right: > 1 })
                    return graph;
                
                foreach (var c in combo)
                {
                    graph[c.NodeA].Add(c.NodeB);
                    graph[c.NodeB].Add(c.NodeA);
                }
            }

            return graph;
        }

        private static (int left, int right) GetGroupSizes(this Dictionary<string, HashSet<string>> graph)
        {
            var nodes = graph.Keys.Count;
            var visited = new HashSet<string>();
            var nodesToVisit = new List<string>
            {
                graph.Keys.First()
            };

            while (nodesToVisit.Any())
            {
                var current = nodesToVisit.First();
                visited.Add(current);
                nodesToVisit.Remove(current);

                foreach (var adjacent in graph[current])
                {
                    if (!visited.Contains(adjacent))
                        nodesToVisit.Add(adjacent);
                }
            }

            return (visited.Count, nodes - visited.Count);
        }

        private static List<List<Edge>> EdgeCombinations(List<Edge> remaining, List<Edge> selected, List<List<Edge>> result)
        {
            if (selected.Count == 3)
            {
                result.Add(selected);
                return result;
            }
            
            if (!remaining.Any())
                return result;

            var edge = remaining.First();
            remaining.Remove(edge);

            EdgeCombinations(new List<Edge>(remaining), new List<Edge>(selected), result);
            EdgeCombinations(new List<Edge>(remaining), new List<Edge>(selected)
            {
                edge
            }, result);

            return result;
        }
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}