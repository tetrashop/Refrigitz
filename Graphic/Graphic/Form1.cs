using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{

public partial class Form1 : Form
{
   int Kind = 0;
   float xp0 = 0, yp0 = 0, xp1 = 0, yp1 = 0, xp2 = 0, yp2 = 0, xp3 = 0, yp3 = 0;
   float xz0 = 0, yz0 = 0, xz1 = 0, yz1 = 0, xz2 = 0, yz2 = 0, xz3 = 0, yz3 = 0;
   float xs0 = 0, ys0 = 0, xs1 = 0, ys1 = 0, xs2 = 0, ys2 = 0;// xs3 = 0, ys3 = 0;
   float xb0 = 0, yb0 = 0, xb1 = 0, yb1 = 0, xb2 = 0, yb2 = 0, xb3 = 0, yb3 = 0;
   float xe0 = 0, ye0 = 0, xe1 = 0, ye1 = 0;
   float xr0 = 0, yr0 = 0, xr1 = 0, yr1 = 0;
   float xm = 0, ym = 0;
   float XP = 0, YP = 0;
   int ColorBox = 0;
   float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep=0;
   private float xprin, yprin;
   bool[] outcode0,outcode1,outcodeOut = new bool[4];
   Shape2D Node=new Shape2D();
   Shape2D Sh = new Shape2D();
   static int ClickMouse =-1;
   bool DrawLine = false;
   bool[] SetValue = new bool[3];
   bool[] SetValueforBezier = new bool[4];
   bool MoveAllow = false;
    bool Trans = false;

   static int ArcCount = 0;
   static int LineCount = 0;
   static int BezierCount = 0;
   static int EllipseCount = 0;
   static int RectangleCount = 0;
   public Form1()
   {
          InitializeComponent();
   }
   private void Form1_Load(object sender, EventArgs e)
   {
        }
   private void Transmition(object sender, float x, float y, float tx, float ty)
   {
       xprin = x + tx;
       yprin = y + ty;
   }
   private void Rotation(object sender, float x, float y, float tx, float Teta)
   {
       xprin = x * (float)(System.Math.Cos(Teta)) - y * (float)(System.Math.Cos(Teta));
       yprin = y * (float)(System.Math.Cos(Teta)) + y * (float)(System.Math.Cos(Teta));        
   }
   private void Scaling(object sender, float x, float y, float sx, float sy)
   {
       xprin = x * sx;
       yprin = y * sy;
   }
   private void MirrorX(object sender, float x, float y)
   {
    xprin = x;
    yprin = -1 * y;
   }
   private void MirrorY(object sender, float x, float y)
   {
    xprin = -1 * x;
    yprin = y;
   }
   private void InversionTransmision(object sender, float x, float y, float tx, float ty)
   {
    xprin = x - tx;
    xprin = y - ty;
   }
   private void InversionRotation(object sender, float x, float y, float Teta)
   {
    xprin = x * (float)(System.Math.Cos(Teta)) + y * (float)(System.Math.Cos(Teta));
    yprin = y * (float)(System.Math.Cos(Teta)) - y * (float)(System.Math.Cos(Teta));
   }
   private void InversionScalling(object sender, float x, float y, float sx, float sy)
   {
    xprin = x / sx;
    yprin = y / sy;
   }
   private void CohenSutherlandLineClipAndDraw(object sender,float x0,float y0,float x1,float y1,float xmin,float xmax,float ymin,float ymax,int value)
   {   
   //Cohne-sutherland clipping algorithm for line P0=(x0,y0) to p!=(x1,y!)
   //and clip rectangle with diagonal from (xmin,ymin) to (xmax,ymax).
   //bool accept,done;
     float x=0,y=0;
     bool done;
    //accept = false;
     done=false;
     CompOutCode(x0, y0, xmin, xmax, ymin, ymax,ref outcode0); CompOutCode(x1, y1,xmin,xmax,ymin,ymax,ref outcode1);
     do {
        if (outcode0[0] == false && outcode0[1] == false && outcode0[2] == false && outcode0[3] == false && outcode1[0] == false && outcode1[1] == false && outcode1[2] == false && outcode1[3] == false)
        {
            //accept = true;
            done = true;
        }
        if ((outcode0[0] & outcode1[0] == false) && (outcode0[1] & outcode1[1] == false) && (outcode0[2] & outcode1[2] == false) && (outcode0[3] & outcode1[3] == false))
            done = true;
        else
        {
         /*Failed both test,so calculate the line segment to clip;
          from an outside point to an intersection with clip edge.*/
            if(!(outcode0[0] == false && outcode0[1] == false && outcode0[2] == false && outcode0[3] == false ))
                  outcodeOut=outcode0;
                else
                outcodeOut=outcode1;
                /*Now find intersection point;
                 use formulas y=y0+slope*(x-x0),x=x0+(1/slope)*(y-y0).*/
                if(outcodeOut[3]){//Divide line at top of clip rectangle
                    x=x0+(x1-x0)*(ymax-y0)/(y1-y0);
                    y=ymax;
                }
                if(outcodeOut[2])//Divide line at bottom of clip rectangle
                {
                    x=x0+(x1-x0)*(ymin-y0)/(y1-y0);
                    y=ymax;
                }
                else if(outcodeOut[1])//Divide line at right edge of clip rectangle
                {
                    y=y0+(y1-y0)*(xmax-x0)/(x1-x0);
                    x=xmax;
                }
                else if(outcodeOut[0])//Divide line at left edge of clip rectangle
                {
                    y=y0+(y1-y0)*(xmin-x0)/(x1-x0);
                    x=xmin;
                }
            /*Now we move outside point to intersection point to clip,
            and get ready for next pass*/
                if (outcodeOut == outcode0)
                {
                    x0 = x;
                    y0 = y;
                }
                else
                {
                    x1 = x;
                    y1 = y;
                }
        }
    } while (!done);
      //if accept then Draw line.
    }
    private void CompOutCode(float x, float y,float xmin,float xmax,float ymin,float ymax,ref bool[] code)
    {
        for(int i=0;i<4;i++)
             code[i] =false;
        if (y > ymax) code[3] =true;
             else if (y < ymin) code[2] = true;
        if (x > xmax) code[0]=true;
             else if (x < xmin) code[0] =true;
    }
    private void aRCToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
            ClickMouse = 0;
            Kind = 1;
    }
    private void mirroToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }    
    private void Form1_Click(object sender, EventArgs e)
    {//This method gets MousePoision.
         }

    private void aRCToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
    {        
    }

    private void Form1_Click_1(object sender, EventArgs e)
    {

        //Shape2D Set = new Shape2D();
        if (ClickMouse == 0)
            DrawLine = false;
        if(ClickMouse==1)
            DrawLine = true;
        ClickMouse++;
        if(ClickMouse!=1)
        if (Kind == 3)
        {
            xp0 = xp1;
            yp0 = yp1;
        }
        xprin = this.PointToClient(Control.MousePosition).X;
        yprin = this.PointToClient(Control.MousePosition).Y;
        //The Mouse Click event;
        if(!MoveAllow){
        if ((Kind != 4)&(Kind!=5))
        {
            if (ClickMouse == 1)
            {
                xp0 = xprin;
                yp0 = yprin;
            }
            if (DrawLine)
            {
                xp1 = xprin;
                yp1 = yprin;
                if (ClickMouse == 2)
                {
                    DrawLine = false;
                    //The Second Point is Setting
                    xs1 = xp1;
                    ys1 = yp1;
                }
            }
        }
        else 
        if(ClickMouse==1)
        {
            xp0 = xprin;
            yp0 = yprin;
        }
            }
    }
    private void aRCToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
    {
        ClickMouse++;     
    }

    private void Form1_MouseMove(object sender, MouseEventArgs e)
    {
        this.DrawExistShape(sender);

        if (Kind == 1)
                this.ArcDraw(sender,this.DetermineColor(sender));
        if (Kind == 2)
                this.LineDraw(sender,this.DetermineColor(sender));
        if (Kind == 3)
            this.LineForBezierDraw(sender,this.DetermineColor(sender));
        if (Kind == 4)
            this.EllipseDraw(sender);
        if (Kind == 5)
            this.RectangleDraw(sender);
        if (Kind == 6)
            this.Pen(sender);
        if((ClickMouse==1)||(ClickMouse==2))
        if (MoveAllow)
              this.MoveAll(sender);
      if (Trans)
          this.TransOperation(sender);
        this.DrawExistShape(sender);         
    }
    private void ArcDraw(object sender,Pen p)
    {
        if(ClickMouse==1)
            this.LineDraw(sender,this.DetermineColor(sender));
        if (ClickMouse == 2)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            xp2 = this.PointToClient(Control.MousePosition).X;
            yp2 = this.PointToClient(Control.MousePosition).Y;        
            R = (float)System.Math.Sqrt(System.Math.Pow((xp1 - xp0), 2) + System.Math.Pow((yp1 - yp0), 2));
            P = (float)System.Math.Sqrt(System.Math.Pow((xp2 - xp0), 2) + System.Math.Pow((yp2 - yp0), 2));
            N = (float)System.Math.Sqrt(System.Math.Pow((xp1 - xp2), 2) + System.Math.Pow((yp1 - yp2), 2));
            TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yp1 - yp0) / (xp1 - yp0))));
            TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
            g.DrawArc(this.DetermineColor(sender), xp0, yp0, (float)System.Math.Abs(xp1 - xp0), (float)System.Math.Abs(yp1 - yp0), TetaStart, TetaSweep);
        }
        //The thirth point is setting.
        if (ClickMouse == 4) {
            xs2 = xp2;
            ys2 = yp2;
            xs1 = xs1 + xp3;
            ys1 = ys1 + yp3;
        }
        if(Kind==1)
            if (ClickMouse == 3) {
                xp3 = this.PointToClient(Control.MousePosition).X-xp0;
                yp3 = this.PointToClient(Control.MousePosition).Y-yp0;
                Graphics g = this.CreateGraphics();
                g.Clear(Color.White);
                //g.DrawArc(this.DetermineColor(sender), xp0, yp0, (float)System.Math.Abs(xp1+xp3 - xp0), (float)System.Math.Abs(yp1+yp3 - yp0), TetaStart, TetaSweep);                                 
                g.DrawArc(this.DetermineColor(sender), xp0, yp0, (float)System.Math.Abs(xp1 + xp3 - xp0), (float)System.Math.Abs(yp1 + yp3 - yp0), TetaStart, TetaSweep);                                 
            }
        if (Kind == 1)
            if (ClickMouse == 4)
            {
                Shape2D Sh = new Shape2D(1,xs0,ys0,xs1,ys1,xs2,ys2,0,0);
                this.GetShapeLast(sender).Start2D = Sh;
                Sh.Redraw = true;
                Sh.Name = "Arc" + System.Convert.ToSingle(ArcCount);
                ArcCount++;
                comboBox1.Items.Add(Sh.Name);
                ClickMouse = 0;
                Kind = 0;
                //SetColor
                Sh.Pc = this.DetermineColor(sender);
                
            }
            //
    }
    private void LineDraw(object sender,Pen p)
    {
        xp1 = this.PointToClient(Control.MousePosition).X;
        yp1 = this.PointToClient(Control.MousePosition).Y;
        //The First point Setting for redraw
        xs0 = xp0;
        ys0 = yp0;                
        Graphics g = this.CreateGraphics();
        if (ClickMouse == 1)
        {
            if (ClickMouse != 2)
                g.Clear(Color.White);
            g.DrawLine(this.DetermineColor(sender), xp0, yp0, xp1, yp1);
        }
        if (Kind == 2)
            if (ClickMouse == 2)
            {
                xs1 = xp1;
                ys1 = yp1;                
                Shape2D Sh = new Shape2D(2,xs0,ys0,xs1,ys1, 0, 0, 0, 0);
                this.GetShapeLast(sender).Start2D = Sh;
                Sh.Name = "Line" + System.Convert.ToSingle(LineCount);
                Sh.Redraw = true;
                LineCount++;
                comboBox1.Items.Add(Sh.Name);
                ClickMouse = 0;
                Kind = 0;
                Sh.Pc = this.DetermineColor(sender);
            }
    }
    private void LineForBezierDraw(object sender,Pen p)
    {
        xp1 = this.PointToClient(Control.MousePosition).X;
        yp1 = this.PointToClient(Control.MousePosition).Y;
        Graphics g = this.CreateGraphics();        
        if (ClickMouse == 1)
        {
            xb0 = xp0;
            yb0 = yp0;            
            g.Clear(Color.White);
            g.DrawLine(this.DetermineColor(sender), xb0, yb0, xp1, yp1);                      
             }
        else
        if (ClickMouse ==2)
        {
//            xs1 = xp0;
  //          ys1 = yp0;
            xb1 = xp0;
            yb1 = yp0;             
            g.Clear(Color.White);
            xp1 = this.PointToClient(Control.MousePosition).X;
            yp1 = this.PointToClient(Control.MousePosition).Y;
            g.DrawLine(this.DetermineColor(sender), xb0, yb0, xb1, yb1);
            g.DrawLine(this.DetermineColor(sender), xb1, yb1, xp1, yp1);
        }
        else
            if (ClickMouse == 3)
            {
                //xs2 = xp0;
                //ys2 = yp0;
                xb2 = xp0;
                yb2 = yp0;                    
                g.Clear(Color.White);
                xp1 = this.PointToClient(Control.MousePosition).X;
                yp1 = this.PointToClient(Control.MousePosition).Y;
                g.DrawLine(this.DetermineColor(sender), xb0, yb0, xb1, yb1);
                g.DrawLine(this.DetermineColor(sender), xb1, yb1, xb2, yb2);
                g.DrawLine(this.DetermineColor(sender), xb2, yb2, xp1, yp1);
            }
            else
               if (ClickMouse == 4)
                {
                  //  xs3 = xp0;
                    //ys3 = yp0;
                    xb3 = xp0;
                    yb3 = yp0;                    
                    g.Clear(Color.White);
                    xp1 = this.PointToClient(Control.MousePosition).X;
                    yp1 = this.PointToClient(Control.MousePosition).Y;
                    g.DrawLine(p, xb0, yb0, xb1, yb1);
                    g.DrawLine(p, xb1, yb1, xb2, yb2);
                    g.DrawLine(p, xb2, yb2, xb3, yb3);
                    g.Clear(Color.White);
                    g.DrawBezier(this.DetermineColor(sender), xb0, yb0, xb1, yb1, xb2, yb2, xb3, yb3);
                }
    if (Kind == 3)
        if (ClickMouse == 4)
        {
            Shape2D Sh = new Shape2D(3, xb0, yb0, xb1, yb1, xb2, yb2, xb3, yb3);
            this.GetShapeLast(sender).Start2D = Sh;
            Sh.Start2D = null;
            Sh.Name = "Bezier" + System.Convert.ToSingle(BezierCount);
            Sh.Redraw = true;
            BezierCount++;
            comboBox1.Items.Add(Sh.Name);
            Kind = 0;
            ClickMouse = 0;
            Sh.Pc = this.DetermineColor(sender);
        }          
        
    }
    private void BezierDraw(object sender)
    {
        Graphics g = this.CreateGraphics();
       this.LineForBezierDraw(sender,this.DetermineColor(sender));
       if(ClickMouse==4)
        g.DrawBezier(this.DetermineColor(sender),xp0,yp0,xp1,yp1,xp2,yp2,xp3,yp3);
        
      
    }
    private void EllipseDraw(object sender) 
    {
        Graphics g = this.CreateGraphics();
        if (ClickMouse == 1)
        {
            xp1 = this.PointToClient(Control.MousePosition).X - 80;
            yp1 = this.PointToClient(Control.MousePosition).Y - 80;
            g.Clear(Color.White);
            g.DrawEllipse(this.DetermineColor(sender), (int)xp0, (int)yp0, xp1, yp1);
        }
        if (ClickMouse == 2)
        {
            xp0 = this.PointToClient(Control.MousePosition).X - 80;
            yp0 = this.PointToClient(Control.MousePosition).Y - 80;
            g.Clear(Color.White);
            g.DrawEllipse(this.DetermineColor(sender), (int)xp0, (int)yp0, xp1, yp1);           
        }
        if (Kind == 4)
            if (ClickMouse == 3)
            {
                Shape2D Sh = new Shape2D(4, xp0, yp0, xp1, yp1, 0, 0, 0, 0);
                this.GetShapeLast(sender).Start2D = Sh;
                Sh.Name = "Ellipse" + System.Convert.ToSingle(EllipseCount);
                Sh.Redraw = true;
                EllipseCount++;
                comboBox1.Items.Add(Sh.Name);
                Kind = 0;
                ClickMouse = 0;
                Sh.Pc = this.DetermineColor(sender);
            }

    }
    private void RectangleDraw(object sender) 
    {
        Graphics g = this.CreateGraphics();
        if (ClickMouse == 1)
        {
            xp1 = (this.PointToClient(Control.MousePosition).X)-50;
            yp1 = (this.PointToClient(Control.MousePosition).Y)-50;
            g.Clear(Color.White);
            g.DrawRectangle(this.DetermineColor(sender),xp0,yp0, xp1, yp1);
        }
        if (ClickMouse == 2)
        {
            xp0 = (this.PointToClient(Control.MousePosition).X)-50;
            yp0 = (this.PointToClient(Control.MousePosition).Y)-50;
            g.Clear(Color.White);
            g.DrawRectangle(this.DetermineColor(sender),xp0,yp0, xp1, yp1);
        }
        if (Kind == 5)
            if (ClickMouse == 3)
            {
                Shape2D Sh = new Shape2D(5, xp0, yp0, xp1, yp1, 0, 0, 0, 0);
                this.GetShapeLast(sender).Start2D = Sh;
                Sh.Name = "Rectangle" + System.Convert.ToSingle(RectangleCount);
                Sh.Redraw = true;
                RectangleCount++;
                comboBox1.Items.Add(Sh.Name);
                Kind = 0;
                ClickMouse = 0;
                Sh.Pc = this.DetermineColor(sender);
            }          
    

    }
    private void Pen(object sender)
    {
            Point2D PoS=new Point2D();               
            Graphics g = this.CreateGraphics();
            xprin = this.PointToClient(Control.MousePosition).X;
            yprin = this.PointToClient(Control.MousePosition).Y;
            //System.Drawing.Pen Pn=new Pen(xprin,yprin);
            g.DrawLine(this.DetermineColor(sender), xprin, yprin, (float)(xprin + 0.5), (float)(yprin + 0.5));
            g.DrawLine(this.DetermineColor(sender), xprin, yprin, (float)(xprin + 0.5), (float)(yprin - 0.5));
            g.DrawLine(this.DetermineColor(sender), xprin, yprin, (float)(xprin - 0.5), (float)(yprin + 0.5));
            g.DrawLine(this.DetermineColor(sender), xprin, yprin, (float)(xprin - 0.5), (float)(yprin - 0.5));
            Point2D Po=new Point2D(xprin,yprin);
            PoS=Sh.StartPoint2D;
            if(PoS!=null)
               while(PoS.StartPoint2D!=null)
                     PoS=PoS.StartPoint2D;
            PoS=Po;
            Po.StartPoint2D=null;        
            if (ClickMouse == 2) {
                ClickMouse = 0;
                Kind = 0;
            }        
        
    
    }
    private void DrawPath(object sender, Pen p) {
        Graphics g = this.CreateGraphics();        

    }
    private void DrawExistShape(object sender)
    {
        
        Shape2D Start = new Shape2D();
        Start = Node;
        Graphics g = this.CreateGraphics();
        while (Start!=null) 
        {

            if (Start.Shap == Shape2D.Shape.Arc)
            {
                //Point2D Dimension = new Point2D();
                //Dimension = Start.
                if (Start.Redraw)
                {
                    if (Start != null)
                    {
                        xz0 = Start.StartPoint2D.GetX();
                        yz0 = Start.StartPoint2D.GetY();
                        xz1 = Start.StartPoint2D.StartPoint2D.GetX();
                        yz1 = Start.StartPoint2D.StartPoint2D.GetY();
                        xz2 = Start.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                        yz2 = Start.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                        float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep;
                        R = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz0), 2) + System.Math.Pow((yz1 - yz0), 2));
                        P = (float)System.Math.Sqrt(System.Math.Pow((xz2 - xz0), 2) + System.Math.Pow((yz2 - yz0), 2));
                        N = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz2), 2) + System.Math.Pow((yz1 - yz2), 2));
                        TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yz1 - yz0) / (xz1 - yz0))));
                        TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
                        g.DrawArc(Start.Pc, xz0, yz0, (float)System.Math.Abs(xz1 - xz0), (float)System.Math.Abs(yz1 - yz0), TetaStart, TetaSweep);
                    }
                }
            }
            if (Start.Shap == Shape2D.Shape.Line)
            {
             if (Start.Redraw)
             {
                 if (Start != null)
                 {
                     xz0 = Start.StartPoint2D.GetX();
                     yz0 = Start.StartPoint2D.GetY();
                     xz1 = Start.StartPoint2D.StartPoint2D.GetX();
                     yz1 = Start.StartPoint2D.StartPoint2D.GetY();
                     g.DrawLine(Start.Pc,xz0,yz0,xz1,yz1);
                 }                
             }            
            }
            if (Start.Shap == Shape2D.Shape.Bezier)
            {
                //Point2D Dimension = new Point2D();
                //Dimension = Start.
                if (Start.Redraw)
                {
                    if (Start != null)
                    {
                        xz0 = Start.StartPoint2D.GetX();
                        yz0 = Start.StartPoint2D.GetY();
                        xz1 = Start.StartPoint2D.StartPoint2D.GetX();
                        yz1 = Start.StartPoint2D.StartPoint2D.GetY();
                        xz2 = Start.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                        yz2 = Start.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                        xz3 = Start.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                        yz3 = Start.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                        g.DrawBezier(Start.Pc, xz0, yz0, xz1, yz1, xz2, yz2, xz3, yz3);
                    }
                }
            }
            if (Start.Shap == Shape2D.Shape.Ellipse)
            {
                //Point2D Dimension = new Point2D();
                //Dimension = Start.
                if (Start.Redraw)
                {
                    if (Start != null)
                    {
                        xe0 = Start.StartPoint2D.GetX();
                        ye0 = Start.StartPoint2D.GetY();
                        xe1 = Start.StartPoint2D.StartPoint2D.GetX();
                        ye1 = Start.StartPoint2D.StartPoint2D.GetY();
                        g.DrawEllipse(Start.Pc, (int)xe0, (int)ye0, xe1, ye1);
                    }
                }
            }
            if (Start.Shap == Shape2D.Shape.Rectangle)
            {
                //Point2D Dimension = new Point2D();
                //Dimension = Start.
                if (Start.Redraw)
                {
                    if (Start != null)
                    {
                        xr0 = Start.StartPoint2D.GetX();
                        yr0 = Start.StartPoint2D.GetY();
                        xr1 = Start.StartPoint2D.StartPoint2D.GetX();
                        yr1 = Start.StartPoint2D.StartPoint2D.GetY();
                        g.DrawRectangle(Start.Pc, (int)xr0, (int)yr0, xr1, yr1);
                    }
                }
            }
       
        Start = Start.Start2D;
        }        
    }
    private Shape2D GetShapeLast(object sender)
    {
        Shape2D Last = new Shape2D();
        Last = this.Node;
        if(Last!=null)
        while (Last.Start2D!=null)
            Last = Last.Start2D;
        return Last;
    }
    private void lToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 2;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 1;
    }
    private void button2_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 2;
    }

    private void bezierToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 3;
        for (int i = 0; i < 3; i++)
            this.SetValue[i] = true;
   
    }

    private void button3_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 3;
        for (int i = 0; i < 3; i++)
            this.SetValue[i] = true;
   
    }

    private void EllipseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Kind = 4;
        ClickMouse = 0;
    }

    private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Kind = 5;
        ClickMouse = 0;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        Kind = 4;
        ClickMouse = 0;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        Kind = 5;
        ClickMouse = 0;
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Obtaining Name.
        String Name = "";
        Name = comboBox1.Text.ToString();
        Shape2D Selecte = new Shape2D();
        Selecte = Node;
        //Founding Object
        while (Selecte != null)
        {
            if (Selecte.Name == this.Name)
                break;
            else
                Selecte = Selecte.Start2D;
        }


    }

    private void button6_Click(object sender, EventArgs e)
    {
        String Name = "";
        Name = comboBox1.Text.ToString();
        Shape2D Selecte = new Shape2D();
        Selecte = Node;
        //Founding Object
        while (Selecte.Start2D != null)
        {
            if (Selecte.Start2D.Name == Name)
            {
                Selecte.Start2D = Selecte.Start2D.Start2D;                
            }
          else
                Selecte = Selecte.Start2D;
        }
        comboBox1.Items.Remove(Name);
        comboBox1.Text = "";
        Graphics g = this.CreateGraphics();
        g.Clear(Color.White);
        this.DrawExistShape(sender);
    }

    private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Shape2D Zoom = new Shape2D();
        Graphics g = this.CreateGraphics();
        Zoom = Node.Start2D;
        while (Zoom != null)
        {
            g.Clear(Color.White);
            if (Zoom.Shap == Shape2D.Shape.Arc)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.StartPoint2D.SetX((float)(1.1 * (xz1 - xz0) + xz0));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(1.1 * (xz1 - xz0) + yz0));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetX((float)(1.1 * (xz2 - xz0) + yz0));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetY((float)(1.1 * (yz2 - yz0) + xz0));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep;
                R = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz0), 2) + System.Math.Pow((yz1 - yz0), 2));
                P = (float)System.Math.Sqrt(System.Math.Pow((xz2 - xz0), 2) + System.Math.Pow((yz2 - yz0), 2));
                N = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz2), 2) + System.Math.Pow((yz1 - yz2), 2));
                TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yz1 - yz0) / (xz1 - yz0))));
                TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
                g.DrawArc(Zoom.Pc, xz0, yz0, (float)System.Math.Abs(xz1 - xz0), (float)System.Math.Abs(yz1 - yz0), TetaStart, TetaSweep);

            }
            if (Zoom.Shap == Shape2D.Shape.Line) 
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(1.1 * xz0));
                Zoom.StartPoint2D.SetY((float)(1.1 * yz0));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                g.DrawLine(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Bezier) 
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(1.1 * xz0));
                Zoom.StartPoint2D.SetY((float)(1.1 * yz0));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz1));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz2));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz2));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz3));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz3));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                g.DrawBezier(Zoom.Pc, xz0, yz0, xz1, yz1, xz2, yz2, xz3, yz3);
            }
            if (Zoom.Shap == Shape2D.Shape.Ellipse) 
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(1.1 * xz0));
                Zoom.StartPoint2D.SetY((float)(1.1 * yz0));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                g.DrawEllipse(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Rectangle) 
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(1.1 * xz0));
                Zoom.StartPoint2D.SetY((float)(1.1 * yz0));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(1.1 * xz1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(1.1 * yz1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                g.DrawRectangle(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            Zoom = Zoom.Start2D;
        }
        g.Clear(Color.White);
        this.DrawExistShape(sender);
    }

    private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Shape2D Zoom = new Shape2D();
        Graphics g = this.CreateGraphics();
        Zoom = Node.Start2D;
        while (Zoom != null)
        {
            g.Clear(Color.White);
            if (Zoom.Shap == Shape2D.Shape.Arc)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();


                Zoom.StartPoint2D.StartPoint2D.SetX((float)((yz1 - yz0) / 1.1 + xz0));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)((xz1 - xz0) / 1.1 + yz0));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep;
                R = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz0), 2) + System.Math.Pow((yz1 - yz0), 2));
                P = (float)System.Math.Sqrt(System.Math.Pow((xz2 - xz0), 2) + System.Math.Pow((yz2 - yz0), 2));
                N = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz2), 2) + System.Math.Pow((yz1 - yz2), 2));
                TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yz1 - yz0) / (xz1 - yz0))));
                TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
                g.DrawArc(Zoom.Pc, xz0, yz0, (float)System.Math.Abs(xz1 - xz0), (float)System.Math.Abs(yz1 - yz0), TetaStart, TetaSweep);

            }
            if (Zoom.Shap == Shape2D.Shape.Line)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(xz0 / 1.1));
                Zoom.StartPoint2D.SetY((float)(yz0 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(xz1 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(yz1 / 1.1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                g.DrawLine(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Bezier)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(xz0 / 1.1));
                Zoom.StartPoint2D.SetY((float)(yz0 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(xz1 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(yz1 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetX((float)(xz2 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetY((float)(yz2 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetX((float)(xz3 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetY((float)(yz3 / 1.1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                g.DrawBezier(Zoom.Pc, xz0, yz0, xz1, yz1, xz2, yz2, xz3, yz3);
            }
            if (Zoom.Shap == Shape2D.Shape.Ellipse)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(xz0 / 1.1));
                Zoom.StartPoint2D.SetY((float)(yz0 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(xz1 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(yz1 / 1.1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                g.DrawEllipse(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Rectangle)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX((float)(xz0 / 1.1));
                Zoom.StartPoint2D.SetY((float)(yz0 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetX((float)(xz1 / 1.1));
                Zoom.StartPoint2D.StartPoint2D.SetY((float)(yz1 / 1.1));

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                g.DrawRectangle(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            Zoom = Zoom.Start2D;
        }
        g.Clear(Color.White);
        this.DrawExistShape(sender);
    }
    private void button7_Click(object sender, EventArgs e)
    {
        this.zoomInToolStripMenuItem_Click(sender, e); ;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        this.zoomOutToolStripMenuItem_Click(sender, e);
    }

    private void moveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        MoveAllow = true;
    }
    private void MoveAll(object sender) 
    {
        Shape2D Zoom = new Shape2D();
        Graphics g = this.CreateGraphics();
        Zoom = Node.Start2D;
        if (ClickMouse == 2)
        {
            MoveAllow = false;
            ClickMouse = 0;
            return;
        }          
        while (Zoom != null)
        {
            float Holdx=0,Holdy=0;
            Holdx=XP;
            Holdy=YP;
            XP = this.PointToClient(Control.MousePosition).X;
            YP = this.PointToClient(Control.MousePosition).Y;
            if (XP > Holdx)
                xm=3;
            if(XP<Holdx)
                xm=-3;
            if(YP>Holdy)
                ym=3;
            if(YP<Holdy)
                ym=-3;            
            g.Clear(Color.White);
            if (Zoom.Shap == Shape2D.Shape.Arc)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();


                Zoom.StartPoint2D.SetX(xz0+xm);
                Zoom.StartPoint2D.SetY(yz0+ym);
                Zoom.StartPoint2D.StartPoint2D.SetX(xz1+xm);
                Zoom.StartPoint2D.StartPoint2D.SetY(yz1+ym);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz2+xm);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz2+ym);

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep;
                R = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz0), 2) + System.Math.Pow((yz1 - yz0), 2));
                P = (float)System.Math.Sqrt(System.Math.Pow((xz2 - xz0), 2) + System.Math.Pow((yz2 - yz0), 2));
                N = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz2), 2) + System.Math.Pow((yz1 - yz2), 2));
                TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yz1 - yz0) / (xz1 - yz0))));
                TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
                g.DrawArc(Zoom.Pc, xz0, yz0, (float)System.Math.Abs(xz1 - xz0), (float)System.Math.Abs(yz1 - yz0), TetaStart, TetaSweep);

            }
            if (Zoom.Shap == Shape2D.Shape.Line)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX(xz0+xm);
                Zoom.StartPoint2D.SetY(yz0+ym);
                Zoom.StartPoint2D.StartPoint2D.SetX(xz1+xm);
                Zoom.StartPoint2D.StartPoint2D.SetY(yz1+ym);

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();

                g.DrawLine(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Bezier)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();

                Zoom.StartPoint2D.SetX(xz0+xm);
                Zoom.StartPoint2D.SetY(yz0+ym);
                Zoom.StartPoint2D.StartPoint2D.SetX(xz1+xm);
                Zoom.StartPoint2D.StartPoint2D.SetY(yz1+ym);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz2+xm);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz2+ym);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz3+xm);
                Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz3+ym);

                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                xz1 = Zoom.StartPoint2D.StartPoint2D.GetX();
                yz1 = Zoom.StartPoint2D.StartPoint2D.GetY();
                xz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz2 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                xz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
                yz3 = Zoom.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
                g.DrawBezier(Zoom.Pc, xz0, yz0, xz1, yz1, xz2, yz2, xz3, yz3);
            }
            if (Zoom.Shap == Shape2D.Shape.Ellipse)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                
                Zoom.StartPoint2D.SetX(xz0+xm);
                Zoom.StartPoint2D.SetY(yz0+ym);
                
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                g.DrawEllipse(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            if (Zoom.Shap == Shape2D.Shape.Rectangle)
            {
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                
                Zoom.StartPoint2D.SetX(xz0+xm);
                Zoom.StartPoint2D.SetY(yz0+ym);
                
                xz0 = Zoom.StartPoint2D.GetX();
                yz0 = Zoom.StartPoint2D.GetY();
                g.DrawRectangle(Zoom.Pc, xz0, yz0, xz1, yz1);
            }
            Zoom = Zoom.Start2D;
        }
        g.Clear(Color.White);
        this.DrawExistShape(sender);   
    
    }

    private void button9_Click(object sender, EventArgs e)
    {
        MoveAllow = true;
        ClickMouse = 0;
    }
    private void TransOperation(object sender) 
    {
        String Name = "";
        Name = comboBox1.Text.ToString();
        Shape2D TransmisionObject = new Shape2D();
        TransmisionObject = Node.Start2D;
        //Founding Object
        if (TransmisionObject == null)
            return;        
        while (TransmisionObject!= null)
        {
            if (TransmisionObject.Name == Name)
                break;
            else
                TransmisionObject = TransmisionObject.Start2D;
        }
        if (TransmisionObject == null)
            return;        
        //exit when the object is null
        Graphics g = this.CreateGraphics();
        
        if (TransmisionObject.Shap == Shape2D.Shape.Arc)
        {
            xz0 = TransmisionObject.StartPoint2D.GetX();
            yz0 = TransmisionObject.StartPoint2D.GetY();
            xz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetX();
            yz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetY();
            xz2 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
            yz2 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
            if (ClickMouse == 1)
            {
                XP = this.PointToClient(Control.MousePosition).X;
                YP = this.PointToClient(Control.MousePosition).Y;

                this.Transmition(sender, xz0, yz0, XP - xz0, YP - yz0);
            }

            float R = 0, P = 0, N = 0, TetaStart = 0, TetaSweep;
            R = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz0), 2) + System.Math.Pow((yz1 - yz0), 2));
            P = (float)System.Math.Sqrt(System.Math.Pow((xz2 - xz0), 2) + System.Math.Pow((yz2 - yz0), 2));
            N = (float)System.Math.Sqrt(System.Math.Pow((xz1 - xz2), 2) + System.Math.Pow((yz1 - yz2), 2));
            TetaStart = (float)((180 / 3.14) * (System.Math.Atan((yz1 - yz0) / (xz1 - yz0))));
            TetaSweep = (float)((180 / 3.14) * System.Math.Acos(((System.Math.Pow(R, 2)) + (System.Math.Pow(P, 2)) - (System.Math.Pow(N, 2))) / (2 * R * P)));
            g.Clear(Color.White);
            g.DrawArc(TransmisionObject.Pc, xprin, yprin, (float)System.Math.Abs(xz1 - xz0), (float)System.Math.Abs(yz1 - yz0), TetaStart, TetaSweep);

            if (ClickMouse == 2)
            {
                TransmisionObject.StartPoint2D.SetX(xprin);
                TransmisionObject.StartPoint2D.SetY(yprin);
                TransmisionObject.StartPoint2D.StartPoint2D.SetX(xz1 + xprin-xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.SetY(yz1 + yprin-yz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz2 + xprin-xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz2 + yprin-yz0);
                Trans = false;
            }


        }
        if (TransmisionObject.Shap == Shape2D.Shape.Line) 
        {
            xz0 = TransmisionObject.StartPoint2D.GetX();
            yz0 = TransmisionObject.StartPoint2D.GetY();
            xz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetX();
            yz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetY();
            if (ClickMouse == 1)
            {
                XP = this.PointToClient(Control.MousePosition).X;
                YP = this.PointToClient(Control.MousePosition).Y;

                this.Transmition(sender, xz0, yz0, XP - xz0, YP - yz0);
            }
            g.Clear(Color.White);
            g.DrawLine(TransmisionObject.Pc, xprin, yprin, xz1 + xprin - xz0, yz1 + yprin - yz0);
            if (ClickMouse == 2)
            {
                TransmisionObject.StartPoint2D.SetX(xprin);
                TransmisionObject.StartPoint2D.SetY(yprin);
                TransmisionObject.StartPoint2D.StartPoint2D.SetX(xz1 + xprin - xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.SetY(yz1 + yprin - yz0);
                Trans = false;
            }

        }
        if (TransmisionObject.Shap == Shape2D.Shape.Bezier) 
        {
            xz0 = TransmisionObject.StartPoint2D.GetX();
            yz0 = TransmisionObject.StartPoint2D.GetY();
            xz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetX();
            yz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetY();
            xz2 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
            yz2 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
            xz3 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetX();
            yz3 = TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.GetY();
            if (ClickMouse == 1)
            {
                XP = this.PointToClient(Control.MousePosition).X;
                YP = this.PointToClient(Control.MousePosition).Y;

                this.Transmition(sender, xz0, yz0, XP - xz0, YP - yz0);
            }
            g.Clear(Color.White);
            g.DrawBezier(TransmisionObject.Pc, xprin, yprin, xz1 + xprin - xz0, yz1 + yprin - yz0, xz2 + xprin - xz0, yz2 + yprin - yz0, xz3 + xprin - xz0, yz3 + yprin - yz0);
            if (ClickMouse == 2)
            {
                TransmisionObject.StartPoint2D.SetX(xprin);
                TransmisionObject.StartPoint2D.SetY(yprin);
                TransmisionObject.StartPoint2D.StartPoint2D.SetX(xz1 + xprin - xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.SetY(yz1 + yprin - yz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz2 + xprin - xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz2 + yprin - yz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetX(xz3 + xprin - xz0);
                TransmisionObject.StartPoint2D.StartPoint2D.StartPoint2D.StartPoint2D.SetY(yz3 + yprin - yz0);
                Trans = false;
            }

        }
        if (TransmisionObject.Shap == Shape2D.Shape.Ellipse) {
            xz0 = TransmisionObject.StartPoint2D.GetX();
            yz0 = TransmisionObject.StartPoint2D.GetY();
            xz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetX();
            yz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetY();
            if (ClickMouse == 1)
            {
                XP = this.PointToClient(Control.MousePosition).X;
                YP = this.PointToClient(Control.MousePosition).Y;

                this.Transmition(sender, xz0, yz0, XP - xz0, YP - yz0);
            }
            g.Clear(Color.White);
            g.DrawEllipse(TransmisionObject.Pc, xprin, yprin, xz1, yz1);
            if (ClickMouse == 2)
            {
                TransmisionObject.StartPoint2D.SetX(xprin);
                TransmisionObject.StartPoint2D.SetY(yprin);
                Trans = false;
            }
        }
        if (TransmisionObject.Shap == Shape2D.Shape.Rectangle) 
        {
            xz0 = TransmisionObject.StartPoint2D.GetX();
            yz0 = TransmisionObject.StartPoint2D.GetY();
            xz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetX();
            yz1 = TransmisionObject.StartPoint2D.StartPoint2D.GetY();
            if (ClickMouse == 1)
            {
                XP = this.PointToClient(Control.MousePosition).X;
                YP = this.PointToClient(Control.MousePosition).Y;

                this.Transmition(sender, xz0, yz0, XP - xz0, YP - yz0);
            }
            g.Clear(Color.White);
            g.DrawRectangle(TransmisionObject.Pc, xprin, yprin, xz1, yz1);
            if (ClickMouse == 2)
            {
                TransmisionObject.StartPoint2D.SetX(xprin);
                TransmisionObject.StartPoint2D.SetY(yprin);
                Trans = false;
            }
           
        }

    } 
    private void transmisionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Trans = true;
        ClickMouse = 0;
        }
       private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void tableLayoutPanel1_Click(object sender, EventArgs e)
    {
       
    }

    private void PictureBox9_Click(object sender, EventArgs e)
    {
        ColorBox = 0;
        this.PictureBox23.BackColor = Color.White;    
    }

    private void PictureBox5_Click(object sender, EventArgs e)
    {
        ColorBox = 1;
        this.PictureBox23.BackColor = Color.Black;    

    }

    private void PictureBox10_Click(object sender, EventArgs e)
    {
        ColorBox = 2;
        this.PictureBox23.BackColor = Color.Brown;    
    }

    private void PictureBox8_Click(object sender, EventArgs e)
    {
        ColorBox = 3;
        this.PictureBox23.BackColor = Color.Silver;    
    }

    private void PictureBox11_Click(object sender, EventArgs e)
    {
        ColorBox = 4;
        this.PictureBox23.BackColor = Color.LightCoral;    
    }

    private void PictureBox12_Click(object sender, EventArgs e)
    {
        ColorBox = 5;
        this.PictureBox23.BackColor = Color.Red;    
    }

    private void PictureBox13_Click(object sender, EventArgs e)
    {
        ColorBox = 6;
        this.PictureBox23.BackColor = Color.OrangeRed;    
    }

    private void PictureBox14_Click(object sender, EventArgs e)
    {
        ColorBox = 7;
        this.PictureBox23.BackColor = Color.Bisque;    
    }

    private void PictureBox18_Click(object sender, EventArgs e)
    {
        ColorBox = 8;
        this.PictureBox23.BackColor = Color.Gold;    
    }

    private void PictureBox17_Click(object sender, EventArgs e)
    {
        ColorBox = 9;
        this.PictureBox23.BackColor = Color.Yellow;    
    }

    private void PictureBox21_Click(object sender, EventArgs e)
    {
        ColorBox = 10;
        this.PictureBox23.BackColor = Color.LawnGreen;    
    }

    private void PictureBox22_Click(object sender, EventArgs e)
    {
        ColorBox = 11;
        this.PictureBox23.BackColor = Color.Aquamarine;    
    }

    private void PictureBox19_Click(object sender, EventArgs e)
    {
        ColorBox = 12;
        this.PictureBox23.BackColor = Color.Blue;    
    }

    private void PictureBox20_Click(object sender, EventArgs e)
    {
        ColorBox = 13;
        this.PictureBox23.BackColor = Color.Fuchsia;    
    }
    private void PictureBox15_Click(object sender, EventArgs e)
    {
        ColorBox = 14;
        this.PictureBox23.BackColor = Color.Pink;    
    }

    private void PictureBox16_Click(object sender, EventArgs e)
    {
        ColorBox = 15;
        this.PictureBox23.BackColor = Color.LightPink;    
    }
    private Pen DetermineColor(object sender)
    {
        Pen p = new Pen(Color.White);
        if (ColorBox == 0)
            p.Color = Color.White;
        if (ColorBox == 1)
            p.Color = Color.Black;
        if (ColorBox == 2)
            p.Color = Color.Brown;
        if (ColorBox == 3)
            p.Color = Color.Silver;
        if (ColorBox == 4)
            p.Color = Color.LightCoral;
        if (ColorBox == 5)
            p.Color = Color.Red;
        if (ColorBox == 6)
            p.Color = Color.OrangeRed;
        if (ColorBox == 7)
            p.Color = Color.Bisque;
        if (ColorBox == 8)
            p.Color = Color.Gold;
        if (ColorBox == 9)
            p.Color = Color.Yellow;
        if (ColorBox == 10)
            p.Color = Color.LawnGreen;
        if (ColorBox == 11)
            p.Color = Color.Aquamarine;
        if (ColorBox == 12)
            p.Color = Color.Blue;
        if (ColorBox == 13)
            p.Color = Color.Fuchsia;
        if (ColorBox == 14)
            p.Color = Color.Pink;
        if (ColorBox == 15)
            p.Color = Color.LightPink;
        return p;
    }

 
    private void Form1_FontChanged(object sender, EventArgs e)
    {
        }

    private void button10_Click(object sender, EventArgs e)
    {
        ClickMouse = 0;
        Kind = 6;
    }

    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {


    }

    private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    
   
}
public class Point2D
 {
       float X, Y;
       public Point2D StartPoint2D;
       public Point2D()
       {
       }
       public Point2D(float X0, float Y0)
       {
           X = X0;
           Y = Y0;
       }
       public float GetX()
       {
           return X;
       }
       public float GetY()
       {
           return Y;
       }
       public void SetX(float X0)
       {
           X = X0;
       }
       public void SetY(float Y0)
       {
           Y = Y0;
       }
   }
   public class Shape2D : Point2D
   {
       public enum Shape
       {
           Arc,
           Line,
           Bezier,
           Ellipse,
           Rectangle,
           Pen,
           Chord,
           Pie,
           None
       }
       public enum ColorShape 
       {
           Red,
           Green,
           Blue,
           Black,
           Yellow             
       }
       public Shape Shap;
       public ColorShape ColorSh;
       public String Name = "";
       public Shape2D Start2D;
       public bool Redraw = false;
       public Pen Pc;
       public Shape2D()
       {
           Start2D = null;
       }
       public Shape2D(int ShapeMode, float X1, float Y1, float X2, float Y2, float X3, float Y3, float X4, float Y4)
       {
           this.Start2D = null;
           if (ShapeMode == 1)//Arc
           {
               Shap = Shape.Arc;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               Point2D Po3 = new Point2D(X3, Y3);
               Point2D Po4 = new Point2D(X4, Y4);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = Po3;
               Po3.StartPoint2D = Po4;
               Po4.StartPoint2D = null;
           }
           if (ShapeMode == 2)//Line
           {
               Shap = Shape.Line;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = null;
           }
           if (ShapeMode == 3)//Bezier
           {
               Shap = Shape.Bezier;
               Point2D Po1 = new Point2D(X1,Y1);
               Point2D Po2 = new Point2D(X2,Y2);
               Point2D Po3 = new Point2D(X3,Y3);
               Point2D Po4 = new Point2D(X4, Y4);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = Po3;
               Po3.StartPoint2D = Po4;
               Po4.StartPoint2D = null;
           }
           if (ShapeMode == 4)//Ellipse
           {
               Shap = Shape.Ellipse;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = null;
           }
           
           if (ShapeMode == 5)//Rectangle
           {
               Shap = Shape.Rectangle;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = null;
                }
           if (ShapeMode == 6)//Chord
           {
               Shap = Shape.Chord;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               Point2D Po3 = new Point2D(X3, Y3);
               Point2D Po4 = new Point2D(X4, Y4);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = Po3;
               Po3.StartPoint2D = Po4;
               Po4.StartPoint2D = null;
           }
           if (ShapeMode == 7)//pie
           {
               Shap = Shape.Pie;
               Point2D Po1 = new Point2D(X1, Y1);
               Point2D Po2 = new Point2D(X2, Y2);
               Point2D Po3 = new Point2D(X3, Y3);
               Point2D Po4 = new Point2D(X4, Y4);
               this.StartPoint2D = Po1;
               Po1.StartPoint2D = Po2;
               Po2.StartPoint2D = Po3;
               Po3.StartPoint2D = Po4;
               Po4.StartPoint2D = null;
           }
       }
   }

}