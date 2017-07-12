using System;

namespace readFiles {

    internal class Algo {
        
        private static int tot = 0, n = 4, nc = 0;
        private static int[] C = new int[n];

        /// <summary>
        /// N皇后
        /// </summary>
        /// <param name="cur"></param>
        public static void search(int curLine) {
            if (curLine == n) {
                tot++;
                Console.WriteLine(string.Join(",",C));
            }
            else
                for (int i = 0; i < n; i++) {
                    bool ok = true;
                    C[curLine] = i;
                    //Console.WriteLine($"第{curLine}行放在第{i}个位置上");
                    for (int j = 0; j < curLine; j++) {
                        // curLine + curIndex == line + lineIndex 是
                        if (C[curLine] == C[j] || curLine - C[curLine] == j - C[j] || curLine + C[curLine] == j + C[j]) {
                            ok = false;
                            break; 
                        }
                    }

                    if (ok) search(curLine + 1); 
                }
        }

        
        public static void Test() {
            
            search(0);
            Console.WriteLine(tot);
            Console.WriteLine();
        }


    }
}