using System;
using ArknightsMap;
using System.Windows.Forms;
using System.Drawing;

namespace ArknightsMapViewer
{
    public class TexturedMapDrawer : WinformMapDrawer
    {
        public TexturedMapDrawer(PictureBox pictureBox, Vector2 scaleFactor, Tile[,] map)
        : base(pictureBox, scaleFactor, map) {}

        protected override void DrawTile(int rowIndex, int colIndex)
        {
            Tile tile = Map[colIndex, rowIndex];
            if (!GlobalDefine.TileInfos.TryGetValue(tile.tileKey, out TileInfo tileInfo))
            {
                MainForm.Instance.Log("Undefined Tile: " + tile.tileKey, MainForm.LogType.Warning);
                tileInfo = new TileInfo
                {
                    backgroundColor = Color.White,
                    text = "",
                    textColor = Color.Black,
                    sprite = null
                };
            }

            if (tileInfo.sprite != null){

                Bitmap bitmap = (Bitmap)PictureBox.BackgroundImage;
                
                Vector2Int tileSize = Helper.GetTileSize(MapScaleFactor);
                Image img = tileInfo.sprite;
                Vector2 scaleFactor = new Vector2(tileSize.x * 0.01f, tileSize.y * 0.01f);
                int imgWidth = (int)Math.Ceiling(img.Width * scaleFactor.x);
                int imgHeight = (int)Math.Ceiling(img.Height * scaleFactor.y);
                Rectangle rectangle = new Rectangle(
                    colIndex * tileSize.x - (int)(tileInfo.spritePivot.x * scaleFactor.x + 0.5f),
                    (MapHeight - rowIndex) * tileSize.y - imgHeight + (int)(tileInfo.spritePivot.y * scaleFactor.y + 0.5f),
                    imgWidth,
                    imgHeight);

                DrawUtil.DrawImage(bitmap, img, rectangle);
                
                //Draw Index
                string indexText = GetIndexText(colIndex, rowIndex);
                DrawUtil.DrawString(bitmap, indexText, rectangle, GlobalDefine.INDEX_FONT, GlobalDefine.TEXT_COLOR, TextFormatFlags.Left | TextFormatFlags.Top);
            }
            else {
                DrawTileBasic(rowIndex, colIndex, tileInfo);
            }

        }
    }
}