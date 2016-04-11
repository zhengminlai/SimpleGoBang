using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 五子棋
/// </summary>
namespace GoBang
{  
    public partial class Form1 : Form
    {
        /// <summary>
        /// 棋盘各点图片
        /// </summary>
        private PictureBox[,] chessPicBox = new PictureBox[15, 15];

        /// <summary>
        /// 15*15的棋盘，point代表坐标
        /// </summary>
        private int[,] chessPointBoard = new int[15, 15];

       /// 搜索深度与宽度，根据玩家选择的电脑等级来定，默认为中
        private static int depth=2;
        private static int width=4;

        //棋子颜色，none表示未放置棋子
        public static int black = 0;
        public static int white = 1;
        public static int none = -1;

        /// <summary>
        /// 二维数组，记录棋盘上各点棋子的颜色
        /// </summary>
        public int[,] chessPointColor = new int[15, 15];

        /// <summary>
        /// 玩家棋子颜色
        /// </summary>
        public int playerChessColor = black;

        /// <summary>
        /// 上一次的鼠标停留点
        /// </summary>
        private Point lastPoint = new Point(-1, -1);

        /// <summary>
        /// 是否是玩家回合
        /// </summary>
        private static bool playerRound = true;

        /// <summary>
        /// 棋子的坐标栈
        /// </summary>
        private Stack<Point> pointStack = new Stack<Point>();

        /// <summary>
        /// 棋子的颜色栈，和坐标栈一一对应
        /// </summary>
        private Stack<int> colorStack = new Stack<int>();

        /// <summary>
        /// 棋盘已下的棋子总数，如果是225,则为平局
        /// </summary>
        private static int totalCount = 0;

        /// <summary>
        /// 电脑AI对象
        /// </summary>
        CompAI comp = new CompAI();

        //用blackChessImg和WhiteChessImg作为图片源
        private static Image img_black = Image.FromFile(System.Environment.CurrentDirectory + "\\ChessImage\\black.bmp");
        Image blackChessImg = new System.Drawing.Bitmap(img_black);

        private static Image img_white = Image.FromFile(System.Environment.CurrentDirectory + "\\ChessImage\\white.bmp");
        Image whiteChessImg = new System.Drawing.Bitmap(img_white);
        private int PlayerChessColor
        {
            get
            {
                return playerChessColor;
            }

            set
            {
                playerChessColor = value;
            }
        }

        public Form1()
        {
            InitializeComponent();
            //绘棋盘格
            chessBoardGroupBox.Paint += new PaintEventHandler(Paint_ChessBoard);
            //初始化棋盘
            InitializeChessBoard();
            //为棋盘绑定鼠标事件
            chessBoardGroupBox.MouseMove += new MouseEventHandler(Board_MouseMove);
            chessBoardGroupBox.MouseClick += new MouseEventHandler(Board_MouseClick);
            this.MouseMove += new MouseEventHandler(MyForm_MouseMove);
            this.Text = "五子棋大战！";
            //释放原图片，将复制的图片作为图片源
            img_white.Dispose();
            img_black.Dispose();
        }
        /// <summary>
        /// 初始化棋盘某点
        /// </summary>
        private void initializeChessPicBox(int x,int y)
        {
            chessPointBoard[x, y] = 0;
            chessPointColor[x, y] = none;
            chessPicBox[x, y] = new PictureBox();
            chessPicBox[x, y].Location = new Point(10 + x * 40, 10 + y * 40);
            chessPicBox[x, y].Size = new Size(40, 40);
            chessPicBox[x, y].BackColor = Color.Transparent;
            chessPicBox[x, y].SizeMode = PictureBoxSizeMode.CenterImage;
            chessPicBox[x, y].Visible = false;
            chessBoardGroupBox.Controls.Add(chessPicBox[x, y]);
        }
       
        /// <summary>
        /// 初始化棋盘
        /// </summary>
        private void InitializeChessBoard()
        {
            totalCount = 0;
            //先清空chessPicBox,两个历史记录栈
            chessBoardGroupBox.Controls.Clear();
            pointStack.Clear();
            colorStack.Clear();
            int x, y;
            for (x = 0; x < 15; x++)
                for (y = 0; y < 15; y++)
                    initializeChessPicBox(x, y);
        }

        /// <summary>
        /// 点击棋盘，下棋
        /// </summary>
        private void Board_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.X - 10) / 40;
            int y = (e.Y - 10) / 40;
            playerPutChess(x, y);
        }

        /// <summary>
        /// 鼠标在窗体内棋盘外时，将红方框去掉
        /// </summary>
        private void MyForm_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics gr = chessBoardGroupBox.CreateGraphics();
            Pen lastUsePen = new Pen(Color.White, 1);//置为白色，看不见
            if (lastPoint.X != -1)
            {
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y - 15, lastPoint.X - 15, lastPoint.Y - 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y - 15, lastPoint.X - 5, lastPoint.Y - 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y - 15, lastPoint.X + 5, lastPoint.Y - 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y - 15, lastPoint.X + 15, lastPoint.Y - 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y + 15, lastPoint.X - 15, lastPoint.Y + 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y + 15, lastPoint.X - 5, lastPoint.Y + 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y + 15, lastPoint.X + 15, lastPoint.Y + 5);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y + 15, lastPoint.X + 5, lastPoint.Y + 15);
                lastPoint.X = -1;
                lastPoint.Y = -1;
            }
        }

        /// <summary>
        /// 鼠标在棋盘移动，添加红方框
        /// </summary>
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            int x, y;
            Graphics gr = chessBoardGroupBox.CreateGraphics();
            Pen lastUsePen = new Pen(Color.White, 1);
            Pen usingPen = new Pen(Color.Red, 1);
            if (lastPoint.X != -1)
            {
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y - 15, lastPoint.X - 15, lastPoint.Y - 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y - 15, lastPoint.X - 5, lastPoint.Y - 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y - 15, lastPoint.X + 5, lastPoint.Y - 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y - 15, lastPoint.X + 15, lastPoint.Y - 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y + 15, lastPoint.X - 15, lastPoint.Y + 5);
                gr.DrawLine(lastUsePen, lastPoint.X - 15, lastPoint.Y + 15, lastPoint.X - 5, lastPoint.Y + 15);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y + 15, lastPoint.X + 15, lastPoint.Y + 5);
                gr.DrawLine(lastUsePen, lastPoint.X + 15, lastPoint.Y + 15, lastPoint.X + 5, lastPoint.Y + 15);
            }
            if (10 < e.X && 10 < e.Y && e.X < 600 && e.Y < 600)
            {
                x = ((e.X - 10) / 40) * 40 + 30;
                y = ((e.Y - 10) / 40) * 40 + 30;
                //四个直角，八个线段
                gr.DrawLine(usingPen, x - 15, y - 15, x - 15, y - 7);
                gr.DrawLine(usingPen, x - 15, y - 15, x - 7, y - 15);
                gr.DrawLine(usingPen, x + 15, y - 15, x + 5, y - 15);
                gr.DrawLine(usingPen, x + 15, y - 15, x + 15, y - 7);
                gr.DrawLine(usingPen, x - 15, y + 15, x - 15, y + 7);
                gr.DrawLine(usingPen, x - 15, y + 15, x - 7, y + 15);
                gr.DrawLine(usingPen, x + 15, y + 15, x + 15, y + 7);
                gr.DrawLine(usingPen, x + 15, y + 15, x + 7, y + 15);
                lastPoint.X = x;
                lastPoint.Y = y;
            }
        }

        /// <summary>
        /// 绘棋盘格
        /// </summary>
        private void Paint_ChessBoard(object sender, PaintEventArgs e)
        {
            int i;
            Graphics gr = e.Graphics;
            Pen drawPen = new Pen(Color.Black, 2);
            SolidBrush brush = new SolidBrush(Color.Red);
            for (i = 0; i < 15; i++)
            {
                gr.DrawLine(drawPen, 30 + i * 40, 30, 30 + i * 40, 590);
                gr.DrawLine(drawPen, 30, 30 + i * 40, 590, 30 + i * 40);
            }
            //将重要的五点用红点标记，其中最中间的用更加显眼的红色标记
            gr.FillEllipse(brush, 306, 306, 8, 8);
            gr.FillEllipse(brush, 147, 147, 6, 6);
            gr.FillEllipse(brush, 467, 147, 6, 6);
            gr.FillEllipse(brush, 147, 467, 6, 6);
            gr.FillEllipse(brush, 467, 467, 6, 6);
        }

        /// <summary>
        /// 玩家在x,y放置颜色为chessColor的棋子
        /// </summary>
        /// <param name="chessColor">棋子颜色</param>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        private void playerPutChess(int x, int y)
        {
            if (playerRound)
            {
                if (playerChessColor == black)
                    chessPicBox[x, y].Image = blackChessImg;
                else
                    chessPicBox[x, y].Image = whiteChessImg;

                chessPicBox[x, y].Visible = true;
                chessPointBoard[x, y] = 1;//玩家放置棋子
                chessPointColor[x, y] = playerChessColor;
                Point p = new Point(x, y);
                totalCount++;
                //将历史记录压栈
                pointStack.Push(p);
                colorStack.Push(playerChessColor);
                playerRound = false;
                //是否为平局
                if (totalCount == 225)
                {
                    MessageBox.Show("平局，游戏结束！");
                    PlayerFirstMenuItem.PerformClick();
                }
                else
                {   //是否决出胜负
                    bool gameOverFlag = isGameOver();
                    if (gameOverFlag)
                    {
                        MessageBox.Show("游戏结束,玩家胜！");
                        PlayerFirstMenuItem.PerformClick();//重新开始
                    }
                    else
                        computerPutChess();//电脑下棋
                }
            }
            else
                MessageBox.Show("请等待电脑落子！");
        }
     
        /// <summary>
        /// 检查游戏是否结束
        /// </summary>
        private bool isGameOver()
        {
            //检查各个方向上是否连成五子
            if (checkFromLeftToRight())
                return true;
            else if (checkFromUpToDown())
                return true;
            else if (checkFromLeftUpToRightDown())
                return true;
            else if (checkFromRightUpToLeftDown())
                return true;
            else return false;
        }
        
        /// <summary>
        /// 左右方向
        /// </summary>
        private bool checkFromLeftToRight()
        {
            bool flag = false;
            for (int x = 0; x < 15; x++)
                for (int y = 0; y < 11; y++)
                    if ( chessPointBoard[x, y] == 1 && chessPointBoard[x, y + 1] == 1
                        && chessPointBoard[x, y + 2] == 1 && chessPointBoard[x, y + 3] == 1
                        && chessPointBoard[x, y + 4] == 1 && chessPointColor[x, y] == chessPointColor[x, y + 1]
                        && chessPointColor[x, y] == chessPointColor[x, y + 2] && chessPointColor[x, y] == chessPointColor[x, y + 3]
                        && chessPointColor[x, y] == chessPointColor[x, y + 4] )
                        return true;
            return flag;
        }
        
        /// <summary>
        /// 上下方向
        /// </summary>
        private bool checkFromUpToDown()
        {
            bool flag = false;
            for (int y = 0; y < 15; y++)
                for (int x = 0; x < 11; x++)
                    if ( chessPointBoard[x, y] == 1 && chessPointBoard[x + 1, y] == 1
                        && chessPointBoard[x + 2, y] == 1 && chessPointBoard[x + 3, y] == 1
                        && chessPointBoard[x + 4, y] == 1 && chessPointColor[x, y] == chessPointColor[x + 1, y]
                        && chessPointColor[x, y] == chessPointColor[x + 2, y] && chessPointColor[x, y] == chessPointColor[x + 3, y]
                        && chessPointColor[x, y] == chessPointColor[x + 4, y])
                        return true;
            return flag;
        }
       
        /// <summary>
        /// 左上->右下方向
        /// </summary>
        private bool checkFromLeftUpToRightDown()
        {
            bool flag = false;
            for (int x = 0; x < 11; x++)
                for (int y = 0; y < 11; y++)
                    if ( chessPointBoard[x, y] == 1 && chessPointBoard[x + 1, y + 1] == 1
                        && chessPointBoard[x + 2, y + 2] == 1 && chessPointBoard[x + 3, y + 3] == 1
                        && chessPointBoard[x + 4, y + 4] == 1 && chessPointColor[x, y] == chessPointColor[x + 1, y + 1]
                        && chessPointColor[x, y] == chessPointColor[x + 2, y + 2] && chessPointColor[x, y] == chessPointColor[x + 3, y + 3]
                        && chessPointColor[x, y] == chessPointColor[x + 4, y + 4])
                        return true;
            return flag;
        }
       
        /// <summary>
        /// 右上->左下方向
        /// </summary>
        private bool checkFromRightUpToLeftDown()
        {
            bool flag = false;
            for (int x = 0; x < 11; x++)
                for (int y = 14 ; y > 3; y--)
                    if (chessPointBoard[x, y] == 1 && chessPointBoard[x + 1, y - 1] == 1
                        && chessPointBoard[x + 2, y - 2] == 1 && chessPointBoard[x + 3, y - 3] == 1
                        && chessPointBoard[x + 4, y - 4] == 1 && chessPointColor[x, y] == chessPointColor[x + 1, y - 1]
                        && chessPointColor[x, y] == chessPointColor[x + 2, y - 2] && chessPointColor[x, y] == chessPointColor[x + 3, y - 3]
                        && chessPointColor[x, y] == chessPointColor[x + 4, y - 4])
                        return true;
            return flag;
        }
      
        /// <summary>
        /// 电脑AI下棋
        /// </summary>
        private void computerPutChess()
        {
            int compColor = (playerChessColor == black) ? white : black;
            
            //AI函数,找到权值最大的棋点
            Point p = comp.findBestPoint(chessPointBoard, chessPointColor, compColor, playerChessColor,depth,width);

            if (compColor == black)
                chessPicBox[p.X, p.Y].Image = blackChessImg;
            else
                chessPicBox[p.X, p.Y].Image = whiteChessImg;
            chessPicBox[p.X, p.Y].Visible = true;
            //电脑放置棋子
            chessPointBoard[p.X, p.Y] = 1;
            chessPointColor[p.X, p.Y] = compColor;
            totalCount++;
            //将历史记录压栈
            pointStack.Push(p);
            colorStack.Push(compColor);
            playerRound = true;
            //是否为平局
            if (totalCount == 225)
            {
                MessageBox.Show("平局，游戏结束！");
                PlayerFirstMenuItem.PerformClick();
            }
            else
            {   //是否决出胜负
                bool gameOverFlag = isGameOver();
                if (gameOverFlag)
                {
                    MessageBox.Show("游戏结束,电脑胜，玩家败！");
                    //重新开始，默认玩家优先
                    PlayerFirstMenuItem.PerformClick();
                }
            }
        }

        /// <summary>
        /// 悔棋
        /// </summary>
        private void regrentButton_Click(object sender, EventArgs e)
        {
            if (colorStack.Count != 0)
            {
                int color = colorStack.Pop();
                Point p = pointStack.Pop();
                //如果是上一回合是玩家回合，电脑还没来得及落子，则只需撤回一次，否则要撤回两次
                if (color == playerChessColor)
                {
                    chessPointBoard[p.X, p.Y] = 0;
                    chessBoardGroupBox.Controls.Remove(chessPicBox[p.X, p.Y]);
                    initializeChessPicBox(p.X, p.Y);
                    totalCount--;
                    playerRound = true;
                }
                else
                {
                    if (colorStack.Count != 0)
                    {
                        colorStack.Pop();
                        Point pt = pointStack.Pop();
                        chessPointBoard[p.X, p.Y] = 0;
                        chessPointBoard[pt.X, pt.Y] = 0;
                        chessBoardGroupBox.Controls.Remove(chessPicBox[p.X, p.Y]);
                        chessBoardGroupBox.Controls.Remove(chessPicBox[pt.X, pt.Y]);
                        initializeChessPicBox(p.X, p.Y);
                        initializeChessPicBox(pt.X, pt.Y);
                        totalCount -= 2;
                        playerRound = true;
                    }
                }
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMenuItem_Click(object sender, EventArgs e)
        {
            //默认玩家优先
            playerRound = true;
            reStart();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        
        /// <summary>
        /// 退出游戏
        /// </summary>
        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            ExitGameButton.PerformClick();
        }
       
        /// <summary>
        /// 玩家先下，为黑棋
        /// </summary>
        private void PlayerFirstMenuItem_Click(object sender, EventArgs e)
        {
            PlayerFirstMenuItem.Checked = true;
            CompFirstMenuItem.Checked = false;
            BlackChessLabel.Text = "玩家";
            WhiteChessLabel.Text = "电脑";
            PlayerChessColor = black;
            playerRound = true;
            reStart();
        }
       
        /// <summary>
        /// 电脑先下，为黑棋
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompFirstMenuItem_Click(object sender, EventArgs e)
        {
            PlayerFirstMenuItem.Checked = false;
            CompFirstMenuItem.Checked = true;
            BlackChessLabel.Text = "电脑";
            WhiteChessLabel.Text = "玩家";
            PlayerChessColor = white;
            playerRound = false;
            //重新开始
            reStart();
            //电脑第一次落子落在中间
            chessPointBoard[7, 7] = 1;
            chessPicBox[7, 7].Image = blackChessImg;
            chessPicBox[7, 7].Visible = true;
            chessPointColor[7, 7] = black;
            Point p = new Point(7, 7);
            totalCount++;
            //将历史记录压栈
            pointStack.Push(p);
            colorStack.Push(black);
            //轮到玩家
            playerRound = true;
        }

        /// <summary>
        /// 清空棋盘，重下
        /// </summary>
        private void reStart()
        {
            InitializeChessBoard();
        }
        
        /// <summary>
        /// 游戏规则
        /// </summary>
        private void GameRuleMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AI五子棋游戏,默认玩家为黑棋先手，在某一直线上连成五子即为胜利！\n\t\tEnjoy Yourself  \\(^o^)/");
        }
        
        /// <summary>
        /// 关于
        /// </summary>
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" 作者:赖正敏.\n 版权所有，违者必究！\n All Rights Reserved By LZM");
        }

        private void LowLevelMenuItem_Click(object sender, EventArgs e)
        {
            depth = 1;
            width = 8;
            StartMenuItem.PerformClick();
        }

        private void MediumLevelMenuItem_Click(object sender, EventArgs e)
        {
            depth = 2;
            width = 8;
            StartMenuItem.PerformClick();
        }

        private void HighLevelMenuItem_Click(object sender, EventArgs e)
        {
            depth = 4;
            width = 8;
            StartMenuItem.PerformClick();
        }
    }
}
