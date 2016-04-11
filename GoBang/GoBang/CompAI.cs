using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GoBang
{
    struct CurrentBoard
    {
        /// <summary>
        /// 棋盘各点
        /// </summary>
        public int[,] chessPoint;

        /// <summary>
        /// 棋盘各点棋子颜色
        /// </summary>
        public int[,] chessColor;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="chess_Point">棋盘各点是否落子</param>
        /// <param name="chess_Color">落子点棋子颜色</param>
        public CurrentBoard(int[,] chess_Point,int[,] chess_Color)
        {
            chessPoint = chess_Point;
            chessColor = chess_Color;
        }
    }
    struct Node:IComparable
    {
        public int x;
        public int y;
        public int value;//权值
        public int win; //是否获胜
        public Node(int _x,int _y,int _value,int _win)
        {
            x = _x;
            y = _y;
            value = _value;
            win = _win;
        }
        public int CompareTo(object obj)
        {
            if(obj is Node)
            {
                return value.CompareTo(((Node)obj).value);
            }
            return 1;
        }
    }
    /// <summary>
    /// 电脑下棋的AI
    /// </summary>
    class CompAI
    {
        int WIN = 1;
        int TIE = 0;//平局
        int LOSE = -1;
        //棋子颜色，none表示未放置棋子
        public static int black = 0;
        public static int white = 1;
        public static int none = -1;
        CurrentBoard currentBoard;
        /// <summary>
        /// 电脑执子颜色
        /// </summary>
        static int compChessColor = none;
        static int playerChessColor = none;

        const int comp = 0;
        const int player = 1;
        //每个位置所占用的分值，越往外越小
        int[,] pointValue=
{
    { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 } ,
    { 0 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 0 } ,
    { 0 , 1 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 4 , 4 , 4 , 4 , 4 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 5 , 5 , 5 , 5 , 5 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 5 , 6 , 6 , 6 , 5 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 6 , 5 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 5 , 6 , 6 , 6 , 5 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 5 , 5 , 5 , 5 , 5 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 4 , 4 , 4 , 4 , 4 , 4 , 4 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 3 , 2 , 1 , 0 } ,
    { 0 , 1 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 2 , 1 , 0 } ,
    { 0 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 0 } ,
    { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }
};
        //对每一种状态所对应的分数
        int[] stateScore = { 0, 100000, 10000, 5000, 3000, 500, 100, 50 };
        //五连子为100000，活四为10000，眠四为5000，活三为3000，眠三为500，活二为100，眠二为50，其余为0

        static int depth = 2;
        static int width = 4;

        private Node BestNode;
        /// <summary>
        /// 2代表电脑和玩家2个角色(0是电脑，1是玩家)，8代表各状态,状态下标与stateScore数组下标对应
        /// </summary>
        int[,] countOfState = new int[2, 8];    
        /// <summary>
        /// 五连子
        /// </summary>
        const int FIVE= 1;
        /// <summary>
        /// 活四
        /// </summary>
        const int FOUR = 2;
        /// <summary>
        /// 眠四
        /// </summary>
        const int SLEEP_FOUR = 3;
        /// <summary>
        /// 活三
        /// </summary>
        const int THREE = 4;
        /// <summary>
        /// 眠三
        /// </summary>
        const int SLEEP_THREE = 5;
        /// <summary>
        /// 活二
        /// </summary>
        const int TWO = 6;
        /// <summary>
        /// 眠二
        /// </summary>
        const int SLEEP_TWO = 7;
        public CompAI()
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 8; j++)
                    countOfState[i, j] = 0;
        }

        /// <summary>
        /// 找最佳点
        /// </summary>
        public Point findBestPoint(int[,] chessPointBoard/*棋盘落子点，有子为1，无子为0*/,int[,] chessColorPoint/*棋盘的棋子颜色*/,int compColor/*电脑的棋子颜色*/,int playerColor/*玩家棋子颜色*/,int _depth,int _width)
        {
            BestNode = new Node();
            currentBoard = new CurrentBoard(chessPointBoard, chessColorPoint);
            compChessColor = compColor;
            playerChessColor = playerColor;
            depth = _depth;
            width = _width;
            BestNode.x = BestNode.y = -1;
            BestNode.win = TIE;
            BestNode.value = 0;
            //Alpha-Beta找最优点
            alphaBetaCutSearch(1,-int.MaxValue,int.MaxValue, compChessColor);

            Point p = new Point();
            p.X = BestNode.x;
            p.Y = BestNode.y;
            return p;
        }
      
        /// <summary>
        /// 用贪心算法找到width个估值较高的点
        /// </summary>
        private void findGoodNodes(int chessColor,Node[] goodNodes,int numOfGoodNodes)
        {
            Node tmpNode = new Node();
            int count = 0;
            int x_min = 14, x_max = 0, y_min = 14, y_max = 0 ;
            reduceSearchRange(ref x_min, ref x_max, ref y_min, ref y_max);
            for (int i = x_min; i <= x_max; i++)
            {
                for (int j = y_min; j <= y_max; j++)
                {
                    if (currentBoard.chessPoint[i, j] == 0)
                    {
                        int if_win = TIE;
                        //试在i，j位置放一枚棋子
                        currentBoard.chessColor[i, j] = chessColor;
                        currentBoard.chessPoint[i, j] = 1;
                        //放置一枚棋子后，为当前角色估分
                        int value = evaluateValue(chessColor, ref if_win);
                        //撤回放置
                        currentBoard.chessColor[i, j] = none;
                        currentBoard.chessPoint[i, j] = 0;

                        tmpNode.x = i;
                        tmpNode.y = j;
                        tmpNode.value = value;
                        tmpNode.win = if_win;

                        //按value值降序排序goodNodes列表
                        if (++count <= numOfGoodNodes)
                        {
                            goodNodes[count - 1] = tmpNode;
                            if (count == numOfGoodNodes)
                            {
                                Array.Sort(goodNodes);
                                Array.Reverse(goodNodes);
                            }
                        }//前几个直接放进去，后面的与里面的比较
                        else
                        {
                            for (int k = 0; k < numOfGoodNodes; k++)
                            {
                                if (value > goodNodes[k].value)
                                {
                                    goodNodes[k] = tmpNode;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }    
        
        /// <summary>
        /// 缩小搜索范围，记录当前棋盘所有棋子的最左最右最上最下点构成的矩形,假设下一步棋的位置不会脱离这个框1步以上。
        /// </summary>
        private void reduceSearchRange(ref int x0,ref int x1,ref int y0,ref int y1)
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 15; j++)
                    if (currentBoard.chessPoint[i, j] == 1)
                    {
                        if (i < x0) x0 = i;
                        if (i > x1) x1 = i;
                        if (j < y0) y0 = j;
                        if (j > y1) y1 = j;
                    }
            if (x0 >= 1)
                x0 -= 1;
            else
                x0 = 0;

            if (x1 <= 13)
                x1 += 1;
            else
                x1 = 14;

            if (y0 >= 1)
                y0 -= 1;
            else
                y0 = 0;

            if (y1 <= 13)
                y1 += 1;
            else
                y1 = 14;
        }

        /// <summary>
        /// alpha-beta剪枝算法
        /// </summary>
        /// <param name="searchDepth">当前搜索深度</param>
        /// <param name="alpha">我方</param>
        /// <param name="beta">敌方</param>
        /// <param name="chessColor">执子颜色</param>
        private int alphaBetaCutSearch(int searchDepth,int alpha,int beta, int chessColor)
        {
            int score; //估值分
            Node[] good_node_arr = new Node[width];
            Node[] badNode = new Node[1];
            badNode[0].x = badNode[0].y = -1;
            badNode[0].win = TIE;
            //为我方找到较好的点
            findGoodNodes(chessColor, good_node_arr ,width);
            //找到对方下一步最好的点，看其是否能赢
            findGoodNodes((chessColor == compChessColor) ? playerChessColor : compChessColor, badNode, 1);
            //若下一步可决胜负，则判断谁先到达五子（如下一步我方能形成活四，二对方可由眠四形成五连，则要堵截）
            if (good_node_arr[0].win == WIN && badNode[0].win == WIN)
            {
                if (searchDepth == 1 && badNode[0].value > good_node_arr[0].value)
                {
                    BestNode = badNode[0];
                    return badNode[0].value;
                }
                if (searchDepth == 1 && badNode[0].value <= good_node_arr[0].value)
                {
                    BestNode = good_node_arr[0];
                    return good_node_arr[0].value;
                }
            }
            //若下一步我方赢
            if(good_node_arr[0].win == WIN && badNode[0].win != WIN)
            {
                BestNode = good_node_arr[0];
                return good_node_arr[0].value;
            }
            //若下一步对方赢
            if(badNode[0].win == WIN && good_node_arr[0].win != WIN)
            {
                BestNode = badNode[0];
                return badNode[0].value;
            }
            //搜索到最后一层，递归结束，开始回溯
            if(searchDepth==depth)
            {
                if(depth==1) BestNode= good_node_arr[0];//如果没有递归
                return ( searchDepth % 2 == 1) ? good_node_arr[0].value : (-good_node_arr[0].value);
            }
            for (int i = 0; i < width; i++)
            {
                currentBoard.chessColor[good_node_arr[i].x,good_node_arr[i].y] = chessColor;
                currentBoard.chessPoint[good_node_arr[i].x, good_node_arr[i].y] = 1;
                //开始递归
                score = -alphaBetaCutSearch(searchDepth + 1, -beta, -alpha, (chessColor == compChessColor) ? playerChessColor : compChessColor);
                currentBoard.chessColor[good_node_arr[i].x, good_node_arr[i].y] = none;
                currentBoard.chessPoint[good_node_arr[i].x, good_node_arr[i].y] = 0;
                //剪枝
                if (score >= beta)
                {
                    return beta;
                }
                //更新最优值
                if (score > alpha)
                {
                    alpha = score;
                    if (searchDepth == 1)
                    {
                        BestNode = good_node_arr[i];
                    }
                }
                
            }
            return alpha;
        }
        
        /// <summary>
        /// 检查某条线上棋子颜色为chessColor的连子状态
        /// </summary>
        private void check(int chessColor,List<int> Line)
        {
            int Role = ((chessColor == compChessColor) ? comp : player);
            int RivalColor = ((Role == comp) ? playerChessColor: compChessColor);
            int left, right;
            int lineSize = Line.Count;
            bool[] unanalysed=new bool[lineSize];

            for (int i = 0; i < lineSize; i++)
                unanalysed[i] = true;//检测该点是否被检测过，避免重复

            for(int i = 0 ;i < lineSize ; i++)
            {
                if (Line[i] == chessColor && unanalysed[i])
                {
                    left = right = i;
                    while (left >= 0)
                    {
                        if (Line[left] != chessColor)
                            break;
                        left--;
                    }
                    while (right < lineSize )
                    {
                        if (Line[right] != chessColor)
                            break;
                        right++;
                    }
                    ++left;
                    --right;
                    for (int index = left; index <= right; ++index)
                        unanalysed[index] = false;
                    if (left == right)
                        unanalysed[left] = true;
                    //如果是5连
                    if (right - left > 3)
                    {
                        countOfState[Role, FIVE]++;
                    }
                    //如果是4连
                    else if (right - left == 3)
                    {
                        //如果是活四_xxxx_
                        if (left > 0 && right < lineSize - 1 && Line[left - 1] == none && Line[right + 1] == none)
                        {
                            countOfState[Role, FOUR]++;
                        }
                        //若是冲四，即oxxxx_或_xxxxo
                        else if (((left == 0 || (left > 0 && Line[left - 1] == RivalColor)) && (right < lineSize - 1 && Line[ right + 1] == none))
                                || ((right == lineSize - 1 || (right < lineSize - 1 && Line[right + 1] == RivalColor)) && (left > 0 && Line[left - 1] == none)))
                        {
                            countOfState[Role, SLEEP_FOUR]++;
                        }
                    }
                    //如果是3连
                    else if (right - left == 2)
                    {
                        //如果是冲四 x_xxx或xxx_x
                        if ((left > 1 && Line[left - 2] == chessColor && Line[left - 1] == none && unanalysed[left - 2]) ||
                            (right < lineSize - 2 && Line[right + 2] == chessColor && Line[right + 1] == none && unanalysed[right + 2]))
                        {
                            countOfState[Role, SLEEP_FOUR]++;
                        }
                        //如果是活三 _xxx__或__xxx_
                        else if (left > 0 && right < lineSize - 1 && Line[left - 1] == none && Line[right + 1] == none && ((left > 1 && Line[left - 2] == none) || (right < lineSize - 2 && Line[right + 2] == none)))
                        {
                            countOfState[Role, THREE]++;
                        }
                        //如果是冲三
                        else
                        {
                            //oxxx__或__xxxo
                            if ( ( (left == 0 || (left > 1 && Line[left - 1] == RivalColor)) && (right < lineSize - 2 && Line[right + 1] == none && Line[right + 2] == none)) 
                               ||( (left > 1 && Line[left - 1] == none && Line[left - 2] == none) && (right == lineSize - 1 || (right < lineSize - 1 && Line[right + 1] == RivalColor))))
                            {
                                countOfState[Role, SLEEP_THREE]++;
                            }
                            
                            //o_xxx_o
                           if( left > 0 && Line[left - 1] == none && (left == 1 || (left > 1 && Line[left - 2] == RivalColor)) && (right < lineSize - 1) && Line[right + 1] == none && (right == (lineSize - 2)|| (right < (lineSize - 2) && Line[right + 2] == RivalColor)))
                           {
                                countOfState[Role, SLEEP_THREE]++;
                           }
                        }
                    }
                    //如果是2连
                    else if (right - left == 1)
                    {
                        //如果是冲四oxx_xx或xx_xxo
                        if ( ( (left == 0 || (left > 0 && Line[left - 1] == RivalColor)) && (right < (lineSize - 3) && Line[right + 1] == none && Line[right + 2] == chessColor && unanalysed[right + 2] && Line[right + 3] == chessColor && unanalysed[right + 3]))
                        || ( (right == (lineSize - 1) || (right < (lineSize - 1) && Line[right + 1] == RivalColor)) && left > 2 && Line[left - 1] == none && Line[left - 2] == chessColor && unanalysed[left - 2] && Line[left - 3] == chessColor && unanalysed[left - 3]))
                        {
                            countOfState[Role,SLEEP_FOUR]++;
                        }
                        //如果是活三 _xx_x_或_x_xx_
                        else if (left > 0 && Line[left - 1] == none && right < lineSize - 1 && Line[right + 1] == none && ((right< lineSize - 3 && Line[right + 2] == chessColor && unanalysed[right+2] && Line[right + 3] == none ) || (left > 2 && Line[left - 2] == chessColor && unanalysed[left - 2] && Line[left - 3] == none)))
                        {
                            countOfState[Role, THREE]++;
                        }
                        //如果是冲三 oxx_x_或_x_xxo
                        else if (((left == 0 || (left> 0 && Line[left - 1] == RivalColor)) && right < lineSize - 3 && Line[right + 1] == none && Line[right + 2] == chessColor && unanalysed[right + 2] && Line[right + 3] == none)
                         || ((right == lineSize - 1 || (right < lineSize - 1 && Line[right + 1] == RivalColor)) && left > 2 && Line[left - 1] == none && Line[left - 2] == chessColor && unanalysed[left - 2] && Line[left - 3] == none))
                        {
                            countOfState[Role,SLEEP_THREE]++;
                        }
                        //如果是冲三 oxx__x或x__xxo
                        else if (((left == 0 || (left > 0 && Line[left - 1] == RivalColor)) && right < lineSize - 3 && Line[right + 1] == none && Line[right + 2] == none && Line[right + 3] == chessColor && unanalysed[right + 3])
                         || ((right == lineSize - 1 || (right < lineSize - 1 && Line[right + 1] == RivalColor)) && left > 2 && Line[left - 1] == none && Line[left - 2] == none && Line[left - 3] == chessColor && unanalysed[left-3]))
                        {
                           countOfState[Role,SLEEP_THREE]++;
                        }
                        //如果是冲三 ox__xx或xx__xo
                        else if ((left > 2 && Line[left - 1] == none && Line[left - 2] == none && Line[left - 3] == chessColor && (left == 3 || (left > 3 && Line[left - 4] == RivalColor)))
                         || (right < lineSize - 3 && Line[right + 1] == none && Line[right + 2] == none && Line[right + 3] == chessColor && (right == lineSize - 4 || (right< lineSize - 4 && Line[right + 4] == RivalColor))))
                        {
                            countOfState[Role, SLEEP_THREE]++;
                        }
                        //如果是活二 __xx__
                        else if (left > 1 && right < lineSize - 2 && Line[left - 1] == none && Line[left - 2] == none && Line[right + 1] == none && Line[right + 2] == none)
                        {
                           countOfState[Role,TWO]++;
                        }
                        //如果是死二 oxx___或___xxo
                        else if (((left== 0 || (left > 0 && Line[left - 1] == RivalColor)) && right < lineSize - 3 && Line[right + 1] == none && Line[right + 2] == none && Line[right + 3] == none) ||
                         ((right == lineSize - 1 || (right < lineSize - 1 && Line[right + 1] == RivalColor)) && left > 2 && Line[left - 1] == none && Line[left - 2] == none && Line[left - 3] == none))
                        {
                            countOfState[Role, SLEEP_TWO]++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 找到各状态的个数
        /// </summary>
        private void find_Num_Of_State()
        {
            List<int> color_row = new List<int>();
            List<int> color_col = new List<int>();
            //上下和左右方向搜索
            for (int i = 0; i < 15; i++)
            {
                color_row.Clear();
                color_col.Clear();
                for (int j = 0; j < 15; j++)
                {
                    color_row.Add(currentBoard.chessColor[i, j]);//要么是none,要么是black，要么是white
                    color_col.Add(currentBoard.chessColor[j, i]);
                }
                check(compChessColor, color_row);
                check(compChessColor, color_col);
                check(playerChessColor, color_row);
                check(playerChessColor, color_col);
            }
            List<int> color_line = new List<int>();
            //右上->左下方向
            for (int sum = 4; sum <= 24; ++sum)
            {
                color_line.Clear();
                for (int i = (sum <= 14) ? sum : 14, j = sum - i; i >= 0 && j <= 14; --i, ++j)
                {
                    color_line.Add(currentBoard.chessColor[i, j]);
                }
                check(compChessColor, color_line);
                check(playerChessColor, color_line);
            }
            //左上->右下方向
            for (int sum = 10; sum >= -10; --sum)
            {
                color_line.Clear();
                for (int i = (sum >= 0) ? sum : 0, j = i - sum; i <= 14 && j <= 14; ++i, ++j)
                {
                    color_line.Add(currentBoard.chessColor[i, j]);
                }
                check(compChessColor, color_line);
                check(playerChessColor, color_line);
            }
        }
        
        /// <summary>
        /// chessColor下完了，为其估分，轮到chessColor对手下棋
        /// </summary>
        /// <param name="chessColor">执子颜色</param>
        /// <param name="whether_win">是否赢</param>
        private int evaluateValue(int chessColor,ref int whether_win)
        {
            int current_player = ((chessColor == compChessColor) ? comp : player);
            int Rival = (current_player == comp) ? player : comp;
            int comp_score = 0, player_score = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 8; j++)
                    countOfState[i, j] = 0;//将估值清零
            find_Num_Of_State();
            //计算分值
            for (int i = 1; i < 8; i++)
            {
                comp_score += countOfState[comp, i] * stateScore[i];
                player_score += countOfState[player, i] * stateScore[i];
            }
            //我方五连，我方赢
            if (countOfState[current_player, FIVE] > 0)
            {
                whether_win = WIN;
                return 200000;
            }
            //敌方五连，我方输
            if(countOfState[Rival,FIVE]>0)
            {
                whether_win = LOSE;
                return -200000;
            }
            //我方活四，我方赢
            if (countOfState[current_player, FOUR] > 0 )
            {
                whether_win = WIN;
                return 180000;
            }
            //敌方四连，我方输
            if(countOfState[Rival, FOUR] > 0 || countOfState[Rival, SLEEP_FOUR] > 0)
            {
                whether_win = LOSE;
                return -180000;
            }
            //我方冲四和活三的数目之和>=2
            if( (countOfState[current_player,SLEEP_FOUR] + countOfState[current_player,THREE] > 1) && countOfState[Rival,THREE]==0 && countOfState[Rival,FOUR]==0 && countOfState[Rival,SLEEP_FOUR]==0)
            {
                whether_win = WIN;
                return 100000;
            }
            //加上棋盘上每个位置的分值
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (currentBoard.chessColor[x, y] != none)
                    {
                        if ((current_player == comp && currentBoard.chessColor[x, y] == chessColor) || current_player == player && currentBoard.chessColor[x, y] != chessColor)
                            comp_score += pointValue[x, y];
                        else
                            player_score += pointValue[x, y];
                    }
                }
            }
            return (current_player == comp) ? (comp_score - player_score) : (player_score - comp_score);
        }
    }
}