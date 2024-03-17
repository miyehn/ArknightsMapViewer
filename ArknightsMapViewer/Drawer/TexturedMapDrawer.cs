using ArknightsMap;
using System.Windows.Forms;
using System.Drawing;

namespace ArknightsMapViewer
{
    public class TexturedMapDrawer : WinformMapDrawer
    {
        public TexturedMapDrawer(PictureBox pictureBox, Vector2 scaleFactor, Tile[,] map)
        : base(pictureBox, scaleFactor, map) {}

        public override void DrawMap()
        {
            InitCanvas();
            /*
            for (int row = 0; row < MapHeight; row++)
            {
                for (int col = 0; col < MapWidth; col++)
                {
                    //DrawTile(row, col);
                }
            }
            */
            RefreshCanvas();
        }
    }
}