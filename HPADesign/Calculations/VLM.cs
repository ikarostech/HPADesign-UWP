﻿using System;
using System.Collections.Generic;
using HPADesign.Models;
using HPADesign.Models.Airfoils;
using HPADesign.Models.Components;
using HPADesign.Models.Components.Wings;

namespace HPADesign.Calculations
{
    /// <summary>
    /// VLM法における翼素
    /// </summary>
    public class WingElement
    {
        public Airfoil airfoil;
        public Pos lf { get; set; }
        public Pos lr { get; set; }
        public Pos rf { get; set; }
        public Pos rr { get; set; }

        public Pos pl { get; set; }
        public Pos pr { get; set; }

        public Pos pcp { get; set; }

        public double phi { get; set; }
        public double seta { get; set; }

        public double ds { get; set; }
        public double dl { get; set; }
        public double dc { get; set; }

        public double dzdx { get; set; }
        public double ganma { get; set; }
        public double Downwash { get; set; }
        public double Cp { get; set; }
    }
    public class VLM : Solver
    {
        List<WingElement> Elements;

        double Alpha { get; set; }

        //VLMコンディションはこちらに
        public double Dihedral { get; set; }
        public double Twist { get; set; }
        public int xnum { get; set; }
        public int ynum { get; set; }
        public double xoffset { get; set; }
        public double ypos { get; set; }
        public double chord { get; set; }

        /// <summary>
        /// wingからElementsを作成
        /// </summary>
        /// <param name="wing"></param>
        public VLM(Wing wing)
        {
            //パネルデータ生成
            /*
            Pos rf, rr, tf, tr;

            double yk, ck, osk, dk, ak, xnumk, ynumk, dam0, dam1, dam2, ykp1, ckp1, oskp1, dkp1, akp1, xnumkp1, ynumkp1;
            */
            
        }

        private Matrix Qizj { get; set; }

        private void SolveQijz()
        {
            Qizj= new Matrix(Elements.Count, Elements.Count);
            for (int i = 0; i < Elements.Count; i++)
            {
                for (int j = 0; j < Elements.Count; j++)
                {
                    Pos[] r = new Pos[3];

                    for (int k = 0; k < 3; k++)
                    {
                        r[k] = new Pos(Elements[j].pr[k] - Elements[j].pl[k],
                            Elements[i].pcp[k] - Elements[j].pl[k],
                            Elements[i].pcp[k] - Elements[j].pr[k]);
                    }
                    double qpsirel = 0;

                    //Opt.TODO
                    for (int k = 0; k < 3; k++)
                    {
                        qpsirel += r[0][k] * (r[1].UnitVector[k] - r[2].UnitVector[k]);
                    }

                    Pos phidrel = Pos.CrossProduct(r[0], r[1]);
                    Pos vabrel = (Pos)(qpsirel * phidrel);
                    //後曳渦(左)
                    Pos vae = new Pos();

                    vae.y = r[1][2] /
                        (new Pos(0, r[1][1], r[1][2]).Magnitude) *
                        (1 + r[1].UnitVector[0]);

                    vae.z = -r[1][1] /
                        new Pos(0, r[1][1], r[1][2]).Magnitude *
                        (1 + r[1].UnitVector[0]);

                    //後曳渦(右)
                    Pos veb = new Pos();
                    veb.y = r[2][2] /
                        (new Pos(0, r[2][1], r[2][2]).Magnitude) *
                        (1 + r[2].UnitVector[0]);

                    veb.z = -r[2][1] /
                        new Pos(0, r[1][1], r[1][2]).Magnitude *
                        (1 + r[1].UnitVector[0]);

                    Pos vij = (Pos)((vabrel + vae + veb) / (4.0 * Math.PI));
                    
                    Matrix Phi = new Matrix(new double[3, 3]
                    {
                        { Cal.Cos(Elements[i].phi), 0, 0 },
                        { 0,Cal.Sin(Elements[i].phi),0},
                        { 0,0,Cal.Cos(Elements[i].phi)}
                    });

                    Matrix Seta = new Matrix(new double[3, 3]
                    {
                        { Cal.Cos(Elements[i].seta), 0, 0 },
                        { 0,Cal.Sin(Elements[i].seta),0},
                        { 0,0,Cal.Cos(Elements[i].seta)}
                    });
                    //qijzを計算
                    Qizj[i, j] = Vector.InnerProduct(Phi * Seta * vij, new Vector(new double[3] { 1, 1, 1 }));
                    
                }
            }
        }
        public void Solve()
        {
            //Qijz行列を作成
            SolveQijz();

            Matrix c = Qizj;

        }
    }
}
