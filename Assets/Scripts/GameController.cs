using UnityEngine;
using GameLogic;
using PlayerInterface;

namespace GameVisuals
{
    public class GameController : MonoBehaviour
    {
        GameModel gameModel;
        public BoardParameters boardParameters;
        public BoardCreator boardCreator;
        public Waves waves;
        public PlayerInputManager playerInputManager;
        public TileSelectionVisuals tileSelectionVisuals;
        public UnitsManager unitsManager;
        public UnitType tmpUnitType;

        public UnitType testStats;

        //private OnMouseHoverCollider currentOnMouseHowerCollider;

        void Start()
        {
            gameModel = new GameModel(boardParameters);
            boardCreator.board = gameModel.gameData.board;
            boardCreator.CreateBoard();
            waves.Initialize();
            tileSelectionVisuals.hexTiles = boardCreator.hexTiles;
            unitsManager.Initialize(boardCreator.hexTiles);
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInputManager.mouseTileChanged)
            {
                tileSelectionVisuals.HighLightTile(playerInputManager.lastMouseHexTile, TileHighlightType.NoHighlight);
                //Debug.Log(playerInputManager.lastMouseHexTile);
                //Debug.Log(playerInputManager.mouseHexTile);
            }
            tileSelectionVisuals.HighLightTile(playerInputManager.mouseHexTile, TileHighlightType.Hover);
            if (Input.GetMouseButton(0))
            {
                tileSelectionVisuals.HighLightTile(playerInputManager.mouseHexTile, TileHighlightType.Selection);
                //gameModel.gameData.board.SetUnitOnPosition(new Unit(testStats, playerInputManager.mouseHexTile, 5, gameModel.gameData.eventsSequence), playerInputManager.mouseHexTile);
                //List<HexVector> possibleTiles = gameModel.gameData.board.FindPossibleMoves(playerInputManager.mouseHexTile);
                //foreach (HexVector possibleTile in possibleTiles)
                //{
                //    Debug.Log(possibleTile);
                //    tileSelectionVisuals.HighLightTile(possibleTile, TileHighlightType.PossibleMove);
                //}
                unitsManager.CreateUnit(playerInputManager.mouseHexTile, tmpUnitType);
            }
        }
    }
}