using System;
using System.Collections.Generic;
using ArknightsMap;
using System.Windows.Forms;
using System.Drawing;

namespace ArknightsMapViewer
{
    public class WinformMapDrawer : IMapDrawer
    {
        public PictureBox PictureBox { get; private set; }
        public Tile[,] Map { get; private set; }

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        protected Vector2 MapScaleFactor;

        public WinformMapDrawer(PictureBox pictureBox, Vector2 scaleFactor, Tile[,] map)
        {
            PictureBox = pictureBox;
            Map = map;

            MapWidth = map.GetLength(0);
            MapHeight = map.GetLength(1);

            MapScaleFactor = scaleFactor;
        }

        public void InitCanvas()
        {
            //Bitmap bitmap = new Bitmap(MapWidth * GlobalDefine.TILE_PIXLE, MapHeight * GlobalDefine.TILE_PIXLE);
            Vector2Int tileSize = Helper.GetTileSize(MapScaleFactor);
            Bitmap bitmap = new Bitmap(MapWidth * tileSize.x, MapHeight * tileSize.y);
            PictureBox.BackgroundImage?.Dispose();
            PictureBox.BackgroundImage = bitmap;
            PictureBox.Width = bitmap.Width;
            PictureBox.Height = bitmap.Height;
        }

        public void RefreshCanvas()
        {
            PictureBox.Refresh();
        }

        public virtual void DrawMap()
        {
            InitCanvas();
            for (int row = 0; row < MapHeight; row++)
            {
                for (int col = 0; col < MapWidth; col++)
                {
                    DrawTile(row, col);
                }
            }
            RefreshCanvas();
        }

        private void DrawTile(int rowIndex, int colIndex)
        {
            Tile tile = Map[colIndex, rowIndex];
            if (!GlobalDefine.TileColor.TryGetValue(tile.tileKey, out Color color))
            {
                //Console.WriteLine("Undefined Tile: " + tile.tileKey);
                MainForm.Instance.Log("Undefined Tile: " + tile.tileKey, MainForm.LogType.Warning);
                color = Color.White;
            }
            GlobalDefine.TileString.TryGetValue(tile.tileKey, out (string, Color) tileString);

            Bitmap bitmap = (Bitmap)PictureBox.BackgroundImage;

            //int length = GlobalDefine.TILE_PIXLE;
            Vector2Int tileSize = Helper.GetTileSize(MapScaleFactor);
            Rectangle rectangle = new Rectangle(colIndex * tileSize.x, (MapHeight - rowIndex - 1) * tileSize.y, tileSize.x, tileSize.y);

            DrawUtil.FillRectangle(bitmap, rectangle, color);
            DrawUtil.DrawRectangle(bitmap, rectangle);

            //Draw TileText
            if (!string.IsNullOrEmpty(tileString.Item1))
            {
                DrawUtil.DrawString(bitmap, tileString.Item1, rectangle, GlobalDefine.TEXT_FONT, tileString.Item2);
            }

            //Draw Index
            string indexText = GetIndexText(colIndex, rowIndex);
            DrawUtil.DrawString(bitmap, indexText, rectangle, GlobalDefine.INDEX_FONT, GlobalDefine.TEXT_COLOR, TextFormatFlags.Right | TextFormatFlags.Bottom);
        }

        private string GetIndexText(int colIndex, int rowIndex)
        {
            return $"{colIndex},{rowIndex}";
            //return $"{(char)('A' + rowIndex)}{colIndex + 1}";
        }
    }
}
