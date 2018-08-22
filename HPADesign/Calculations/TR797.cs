using HPADesign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Calculations
{
    
    class TR797
    {
        public int CN { get; set; }
        public int RN { get; set; }
        public double Lift { get; set; }

        public double CruiseVel
        {
            get;set;
        }



        public double span { get { return span; } set { span = value; le = value / 2; } }
        //片翼の
        public double le { get { return le; } set { le = value; span = value * 2; } }

        public Distribution LiftDistribution { get; set; }
        public Distribution TheoricalLiftDistribution { get; set; }

        //楕円分布
        public Distribution EllipseLiftDistribution
        {
            get
            {
                var result = new Distribution();

                return result;
            }
        }
        public void CAL(double beta)
        {
            /*
             * Optimum Design of Nonplanar wings-Minimum Induced Drag
             * for A Given Lift and Wing Root Bending Moment(NAL TR-797)
             * 
             * Original MATLAB based program Created by Takahiro Inagawa on 2013-2-28.
             * https://gist.github.com/ina111/5053876
             * Copyright (c) 2013 Takahiro Inagawa. All rights reserved.
             * 
             * Reedited for C# by Ryo Asano on 2017-9-19
             */

            double[] delta_S = Cal.linspace(le / CN / 2, le / CN / 2, CN);

            double[] y = Cal.linspace(delta_S[0], le - delta_S[CN - 1], CN);
            double[] z = Cal.linspace(0, 0, CN);
            double[] fai = Cal.linspace(0, 0, CN);

            double[,] ydash = new double[CN, CN];
            double[,] zdash = new double[CN, CN];
            double[,] y2dash = new double[CN, CN];
            double[,] z2dash = new double[CN, CN];

            double[,] R2plus = new double[CN, CN];
            double[,] R2minus = new double[CN, CN];
            double[,] Rdash2plus = new double[CN, CN];
            double[,] Rdash2minus = new double[CN, CN];

            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    ydash[i, j] = (y[i] - y[j]) * Cal.Cos(fai[j]) + (z[i] - z[j]) * Cal.Sin(fai[j]);
                    zdash[i, j] = -(y[i] - y[j]) * Cal.Sin(fai[j]) + (z[i] - z[j]) * Cal.Cos(fai[j]);
                    y2dash[i, j] = (y[i] + y[j]) * Cal.Cos(fai[j]) - (z[i] - z[j]) * Cal.Sin(fai[j]);
                    z2dash[i, j] = (y[i] - y[j]) * Cal.Sin(fai[j]) + (z[i] - z[j]) * Cal.Cos(fai[j]);

                    R2plus[i, j] = Math.Pow((ydash[i, j] - delta_S[j]), 2) + Math.Pow(zdash[i, j], 2);
                    R2minus[i, j] = Math.Pow((ydash[i, j] + delta_S[j]), 2) + Math.Pow(zdash[i, j], 2);
                    Rdash2plus[i, j] = Math.Pow((y2dash[i, j] + delta_S[j]), 2) + Math.Pow(z2dash[i, j], 2);
                    Rdash2plus[i, j] = Math.Pow((y2dash[i, j] - delta_S[j]), 2) + Math.Pow(z2dash[i, j], 2);
                }
            }

            double[,] Q = new double[CN, CN];

            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    Q[i, j] = -1 / (2 * Math.PI) * (((ydash[i, j] - delta_S[j]) / R2plus[i, j]
                       - (ydash[i, j] + delta_S[j]) / R2minus[i, j]) * Cal.Cos(fai[i] - fai[j])
                       + (zdash[i, j] / R2plus[i, j] - zdash[i, j] / R2minus[i, j]) * Cal.Sin(fai[i] - fai[j])
                       + ((y2dash[i, j] - delta_S[j]) / Rdash2minus[i, j]
                       - (y2dash[i, j] + delta_S[j]) / Rdash2plus[i, j]) * Cal.Cos(fai[i] + fai[j])
                       + (z2dash[i, j] / Rdash2minus[i, j] - z2dash[i, j] / Rdash2plus[i, j])
                       * Cal.Sin(fai[i] + fai[j]));
                }
            }

            //Normalization
            double[] delta_sigma = new double[CN];
            double[] eta = new double[CN];
            double[,] etadash = new double[CN, CN];
            double[,] eta2dash = new double[CN, CN];
            double[] zeta = new double[CN];
            double[,] zetadash = new double[CN, CN];
            double[,] zeta2dash = new double[CN, CN];
            double[,] gamma2plus = new double[CN, CN];
            double[,] gamma2minus = new double[CN, CN];
            double[,] gammadash2plus = new double[CN, CN];
            double[,] gammadash2minus = new double[CN, CN];

            for (int i = 0; i < CN; i++)
            {
                delta_sigma[i] = delta_S[i] / le;
                eta[i] = y[i] / le;
                zeta[i] = z[i] / le;
                for (int j = 0; j < CN; j++)
                {
                    etadash[i, j] = ydash[i, j] / le;
                    eta2dash[i, j] = y2dash[i, j] / le;
                    zetadash[i, j] = zdash[i, j] / le;
                    zeta2dash[i, j] = z2dash[i, j] / le;
                    gamma2plus[i, j] = R2plus[i, j] / Math.Pow(le, 2);
                    gamma2minus[i, j] = R2minus[i, j] / Math.Pow(le, 2);
                    gammadash2plus[i, j] = Rdash2plus[i, j] / Math.Pow(le, 2);
                    gammadash2minus[i, j] = Rdash2minus[i, j] / Math.Pow(le, 2);
                }
            }

            double[,] q = new double[CN, CN];

            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    q[i, j] = -1 / (2 * Math.PI) * (((etadash[i, j] - delta_sigma[j]) / gamma2plus[i, j]
                       - (etadash[i, j] + delta_sigma[j]) / gamma2minus[i, j]) * Cal.Cos(fai[i] - fai[j])
                       + (zetadash[i, j] / gamma2plus[i, j] - zetadash[i, j] / gamma2minus[i, j]) * Cal.Sin(fai[i] - fai[j])
                       + ((eta2dash[i, j] - delta_sigma[j]) / gammadash2minus[i, j]
                       - (eta2dash[i, j] + delta_sigma[j]) / gammadash2plus[i, j]) * Cal.Cos(fai[i] + fai[j])
                       + (zeta2dash[i, j] / gammadash2minus[i, j] - zeta2dash[i, j] / gammadash2plus[i, j])
                       * Cal.Sin(fai[i] + fai[j]));
                }
            }

            double BendingMoment_elpl = 2 / 3 / Math.PI * le * Lift;
            double InducedDrag_elpl = Math.Pow(Lift, 2) / (2 * Math.PI * Cal.rho * Math.Pow(CruiseVel, 2) * Math.Pow(le, 2));
            double Vn_elpl = Lift / (2 * Math.PI * Cal.rho * CruiseVel * Math.Pow(le, 2));

            double[] c = new double[CN];
            double[] b = new double[CN];
            double[,] A = new double[CN, CN];
            for (int i = 0; i < CN; i++)
            {
                c[i] = 2 * Cal.Cos(fai[i]) * delta_sigma[i];
                b[i] = 3 * Math.PI / 2 * (eta[i] * Cal.Cos(fai[i]) + zeta[i] * Cal.Sin(fai[i])) * delta_sigma[i];
                for (int j = 0; j < CN; j++)
                {
                    A[i, j] = Math.PI * q[i, j] * delta_sigma[i];
                }
            }
            //共役転置との積を求める
            double[,] AAA = new double[CN, CN];

            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    AAA[i, j] = A[i, j] + A[j, i];
                }
            }

            double[] ccc = new double[CN];
            double[] bbb = new double[CN];

            for (int i = 0; i < CN; i++)
            {
                ccc[i] = -c[i];
                bbb[i] = -b[i];
            }
            double[,] LeftMat = new double[CN + 2, CN + 2];
            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    LeftMat[i, j] = AAA[i, j];
                }
                LeftMat[CN, i] = ccc[i];
                LeftMat[CN + 1, i] = bbb[i];

                LeftMat[i, CN] = ccc[i];
                LeftMat[i, CN + 1] = bbb[i];
            }
            LeftMat[CN, CN] = 0;
            LeftMat[CN, CN + 1] = 0;
            LeftMat[CN + 1, CN] = 0;
            LeftMat[CN + 1, CN + 1] = 0;

            double[] RightMat = new double[CN + 2];
            for (int i = 0; i < CN; i++)
            {
                RightMat[i] = 0;
            }
            RightMat[CN] = -1;
            RightMat[CN + 1] = -beta;


            //連立方程式を解く
            //x = SolveMat
            //LeftMat * SolveMat = RightMatとなるようにSolveMatを確定する
            //掃き出し法かなぁ


            //前進消去
            for (int i = 0; i < CN + 2; i++)
            {
                //外角成分の選択、この値で行成分を正規化
                double pivot = LeftMat[i, i];
                for (int j = 0; j < CN + 2; j++)
                {
                    LeftMat[i, j] = (1 / pivot) * LeftMat[i, j];
                }
                RightMat[i] = (1 / pivot) * RightMat[i];
                for (int j = i + 1; j < CN + 2; j++)
                {
                    double mul = LeftMat[j, i];
                    for (int n = i; n < CN + 2; n++)
                    {
                        LeftMat[j, n] -= mul * LeftMat[i, n];
                    }
                    RightMat[j] -= mul * RightMat[i];
                }
            }

            //後進代入
            for (int i = CN + 2 - 1; i > 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double mul = LeftMat[k, i];
                    for (int n = i; n < CN + 2; n++)
                    {
                        LeftMat[k, n] -= mul * LeftMat[i, n];
                    }
                    RightMat[k] -= mul * RightMat[i];
                }
            }
            //移し替える
            double[] SolveMat = new double[CN + 2];
            for (int i = 0; i < CN + 2; i++)
            {
                SolveMat[i] = RightMat[i];
            }
            double[] g = new double[CN];
            double[] mu = new double[] { SolveMat[CN], SolveMat[CN + 1] };

            for (int i = 0; i < CN; i++)
            {
                g[i] = SolveMat[i];
            }

            double efficiency = 0;

            double[] te = new double[CN];
            for (int i = 0; i < CN; i++)
            {
                double temp = 0;
                for (int j = 0; j < CN; j++)
                {
                    temp += g[j] * A[i, j];
                }
                efficiency += temp * g[i];
            }
            efficiency = 1 / efficiency;

            double[] Gamma = new double[CN];
            for (int i = 0; i < CN; i++)
            {
                Gamma[i] = g[i] * Lift / (2 * le * Cal.rho * CruiseVel);
            }
            double InducedDrag = InducedDrag_elpl / efficiency;

            double[] Local_Lift = new double[CN];
            for (int i = 0; i < CN; i++)
            {
                Local_Lift[i] = 4 * Cal.rho * CruiseVel * Gamma[i] * Cal.Cos(fai[i]);
            }
            double[] Vn = Cal.linspace(0, 0, CN);
            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    Vn[i] += Q[i, j] * Gamma[j];
                }
            }
            double[] Local_InducedDrag = new double[CN];
            for (int i = 0; i < CN; i++)
            {
                Local_InducedDrag[i] = Cal.rho * Gamma[i] * Vn[i];
            }

            double Lift0_elpl = 2 * Lift / Math.PI / le;
            double Gamma0_elpl = Lift0_elpl / (Cal.rho * CruiseVel * Cal.Cos(fai[0]));
            double[] Lift_elpl = new double[CN];
            double[] Gamma_elpl = new double[CN];
            double[] Local_InducedDrag_elpl = new double[CN];
            for (int i = 0; i < CN; i++)
            {
                Lift_elpl[i] = 4 * Lift0_elpl * Math.Sqrt(1 - Math.Pow((y[i] / le), 2));
                Gamma_elpl[i] = Gamma0_elpl * Math.Sqrt(1 - Math.Pow(y[i] / le, 2));
                Local_InducedDrag_elpl[i] = 2 * Cal.rho * Gamma_elpl[i] * Vn_elpl;
            }

            double[] Local_BendingMoment = Cal.linspace(0, 0, CN);
            double[] Local_BendingMoment_elpl = Cal.linspace(0, 0, CN);


            for (int i = 0; i < CN; i++)
            {
                for (int j = 0; j < CN; j++)
                {
                    Local_BendingMoment[i] += Local_Lift[j] * (y[j] - y[i]);
                    Local_BendingMoment_elpl[i] += Lift_elpl[j] * (y[j] - y[i]);
                }
            }
            //double
        }

    }
}
