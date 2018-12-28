using System;
using System.Collections.Generic;

namespace HPADesign.Models
{
    /// <summary>
    /// 一次線形材料の定義
    /// </summary>
    public interface ILineStructure
    {

    }
    /// <summary>
    /// 四角形上メッシュの構造に関するインターフェース
    /// </summary>
    public interface IBlockMeshStructure
    {

    }
    /// <summary>
    /// プリプレグの物性について管理します
    /// </summary>
    public class CarbonPrepreg : ILineStructure
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密度
        /// </summary>
        public double Density { get; set; }
        /// <summary>
        /// 厚さ
        /// </summary>
        public double Thin { get; set; }
        /// <summary>
        /// 積層角度
        /// </summary>
        public double Degree { get; set; }
        /// <summary>
        /// 弾性係数EI
        /// </summary>
        public double ElasticityModulus { get; set; }
        /// <summary>
        /// 曲げ強さ
        /// </summary>
        public double BendingStrength { get; set; }
    }
    public class Spar
    {
        public double Length { get; set; }
        /// <summary>
        /// 部分積層終了位置
        /// </summary>
        public double PartLayerEnd { get; set; }
        /// <summary>
        /// 部分積層数、角度は45度から5度ずつ引かれ、25度までを限界とする
        /// </summary>
        public int PartLayer { get; set; }
        public double InnerDiameter { get; set; }
        public double OuterDiameter
        {
            get
            {
                double result = InnerDiameter;
                foreach (CarbonPrepreg l in Layer)
                    result += l.Thin;
                return result;
            }
        }

        public List<CarbonPrepreg> Layer { get; set; }
        public int Count { get { return Layer.Count; } }

        /// <summary>
        /// 桁の曲げ強度を返します。部分積層部や積層角度がついた層は強度に計上しません(Strict条件)
        /// </summary>
        public double BendingStrength
        {
            get
            {
                double result = 0;
                double _innerdiameter = InnerDiameter / 2;
                for (int i = 0; i < Count; i++)
                {
                    CarbonPrepreg l = Layer[i];
                    double _outerdiameter = _innerdiameter + l.Thin;
                    if (PartLayer < i && i < Count - 1)
                    {
                        //強度計算

                        //断面係数の算出
                        double sectionmodulus = (Math.Pow(_outerdiameter, 4) - Math.Pow(_innerdiameter, 4)) * Math.PI / (32 * _outerdiameter);
                        result += l.BendingStrength * sectionmodulus;
                    }
                    _innerdiameter = _outerdiameter;
                }
                return result;
            }
        }
        public double ElasticityModulus
        {
            get
            {
                double result = 0;
                double _innerdiameter = InnerDiameter / 2;
                for (int i = 0; i < Count; i++)
                {
                    CarbonPrepreg l = Layer[i];
                    double _outerdiameter = _innerdiameter + l.Thin;
                    double section2dmoment;
                    if (1 < i && i <= PartLayer)
                        section2dmoment = Math.Cos((45 - 5 * (i - 2)) / 180 * Math.PI) * (Math.Pow(_outerdiameter, 4) - Math.Pow(_innerdiameter, 4)) * Math.PI / 64;
                    else
                        section2dmoment = (Math.Pow(_outerdiameter, 4) - Math.Pow(_innerdiameter, 4)) * Math.PI / 64;
                    result += section2dmoment * l.ElasticityModulus;
                    _innerdiameter = _outerdiameter;
                }
                return result;
            }
        }
    }
}
