using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Checkers
{
    public class Gameplay
    {
        private bool m_PlayerTurn = true;
        private List<Movement> m_MovementsArray;
        private Soldier m_Solider;

        public Gameplay()
        {
            m_Solider = new Soldier();
        }

        public bool PlayerTurn
        {
            get
            {
                return m_PlayerTurn;
            }
        }

        public void ChangeTurnOfPlayer()
        {
            m_PlayerTurn = !m_PlayerTurn;
        }

        public bool CheckIfSourceHasPawn(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            bool checkIfSourceHasPawn = true;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.Space)
            {
                checkIfSourceHasPawn = false;
            }

            return checkIfSourceHasPawn;
        }

        public bool IsKing(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            bool isKing = false;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoldierK || i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderU)
            {
                isKing = true;
            }

            return isKing;
        }

        public bool CheckValidInput(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool checkValidInput = false;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX)
            {
                if (i_RowSource - i_RowDest == 1)
                {
                    if (Math.Abs(i_ColDest - i_ColSource) == 1)
                    {
                        checkValidInput = true;
                    }
                }
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO)
            {
                if (i_RowDest - i_RowSource == 1)
                {
                    if (Math.Abs(i_ColDest - i_ColSource) == 1)
                    {
                        checkValidInput = true;
                    }
                }
            }
            else if (IsKing(i_GameBoard, i_RowSource, i_ColSource))
            {
                if (Math.Abs(i_RowDest - i_RowSource) == 1)
                {
                    if (Math.Abs(i_ColDest - i_ColSource) == 1)
                    {
                        checkValidInput = true;
                    }
                }
            }

            return checkValidInput;
        }

        public bool IsMovementForward(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool isMovementDiagonallyFoward = false;

            if (Math.Abs(i_RowDest - i_RowSource) == 1)
            {
                if (CheckValidInput(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest))
                {
                    isMovementDiagonallyFoward = true;
                }
            }
            else if (Math.Abs(i_RowDest - i_RowSource) == 2)
            {
                if (IsInputJumpValid(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest))
                {
                    isMovementDiagonallyFoward = true;
                }
            }

            return isMovementDiagonallyFoward;
        }

        public bool IsTargetEmpty(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool isTargetSquere = false;

            if (i_GameBoard.GetBoard[i_RowDest, i_ColDest] == m_Solider.Space)
            {
                isTargetSquere = true;
            }

            return isTargetSquere;
        }

        public bool CheckRightMove(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            string sourcePawn, destPawn, nextButton;
            bool isRightMove = false;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX)
            {
                if (i_RowDest == i_RowSource - 2 && i_ColDest == i_ColSource + 2)
                {
                    isRightMove = true;
                }
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO)
            {
                if (i_RowDest == i_RowSource + 2 && i_ColDest == i_ColSource - 2)
                {
                    isRightMove = true;
                }
            }
            else if (IsKing(i_GameBoard, i_RowSource, i_ColSource))
            {
                sourcePawn = i_GameBoard.GetBoard[i_RowSource, i_ColSource];
                nextButton = i_GameBoard.GetBoard[i_RowDest, i_ColDest];
                if (i_RowSource - 2 >= 0 && i_ColSource + 2 < i_GameBoard.BoardSize)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1];
                    if (i_RowDest == i_RowSource - 2 && i_ColDest == i_ColSource + 2 && CheckIfPawnEatValid(sourcePawn, destPawn, nextButton))
                    {
                        isRightMove = true;
                    }
                }

                if (i_RowSource + 2 < i_GameBoard.BoardSize && i_ColSource - 2 >= 0 && !isRightMove)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1];
                    if (i_RowDest == i_RowSource + 2 && i_ColDest == i_ColSource - 2 && CheckIfPawnEatValid(sourcePawn, destPawn, nextButton))
                    {
                        isRightMove = true;
                    }
                }
            }

            return isRightMove;
        }

        public bool CheckLeftMove(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            string sourcePawn, destPawn, nextButton;
            bool isLeftMove = false;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX)
            {
                if (i_RowDest == i_RowSource - 2 && i_ColDest == i_ColSource - 2)
                {
                    isLeftMove = true;
                }
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO)
            {
                if (i_RowDest == i_RowSource + 2 && i_ColDest == i_ColSource + 2)
                {
                    isLeftMove = true;
                }
            }
            else if (IsKing(i_GameBoard, i_RowSource, i_ColSource))
            {
                sourcePawn = i_GameBoard.GetBoard[i_RowSource, i_ColSource];
                nextButton = i_GameBoard.GetBoard[i_RowDest, i_ColDest];
                if (i_RowSource - 2 >= 0 && i_ColSource - 2 >= 0)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1];
                    if (i_RowDest == i_RowSource - 2 && i_ColDest == i_ColSource - 2 && CheckIfPawnEatValid(sourcePawn, destPawn, nextButton))
                    {
                        isLeftMove = true;
                    }
                }

                if (i_RowSource + 2 < i_GameBoard.BoardSize && i_ColSource + 2 < i_GameBoard.BoardSize && !isLeftMove)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1];
                    if (i_RowDest == i_RowSource + 2 && i_ColDest == i_ColSource + 2 && CheckIfPawnEatValid(sourcePawn, destPawn, nextButton))
                    {
                        isLeftMove = true;
                    }
                }
            }

            return isLeftMove;
        }

        public bool IsInputJumpValid(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool isInputJumpDiagonalLegit = false;
            bool isRightDiagonalPossible = IsRightJumpPossible(i_GameBoard, i_RowSource, i_ColSource);
            bool isLeftDiagonalPossible = IsLeftJumpPossible(i_GameBoard, i_RowSource, i_ColSource);

            if (isRightDiagonalPossible && isLeftDiagonalPossible)
            {
                isInputJumpDiagonalLegit = CheckRightMove(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
                if (!isInputJumpDiagonalLegit)
                {
                    isInputJumpDiagonalLegit = CheckLeftMove(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
                }
            }
            else if (isRightDiagonalPossible)
            {
                isInputJumpDiagonalLegit = CheckRightMove(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
            }
            else if (isLeftDiagonalPossible)
            {
                isInputJumpDiagonalLegit = CheckLeftMove(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
            }

            return isInputJumpDiagonalLegit;
        }

        public bool CheckAnotherJumpsAvailable(GameBoard i_GameBoard)
        {
            bool isCheckAnotherJumpsAvailable = false;
            string pawn, king;

            if (m_PlayerTurn)
            {
                pawn = m_Solider.SoliderX;
                king = m_Solider.SoldierK;
            }
            else
            {
                pawn = m_Solider.SoliderO;
                king = m_Solider.SoliderU;
            }

            for (int i = 0; i < i_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize; j++)
                {
                    if (i_GameBoard.GetBoard[i, j] == pawn || i_GameBoard.GetBoard[i, j] == king)
                    {
                        if (IsLeftJumpPossible(i_GameBoard, i, j) || IsRightJumpPossible(i_GameBoard, i, j))
                        {
                            isCheckAnotherJumpsAvailable = true;
                            break;
                        }
                    }
                }
            }

            return isCheckAnotherJumpsAvailable;
        }

        public bool CheckIfCanMoveFromMostRightCol(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            bool isCheckIfPawnMostRightCol = true;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX)
            {
                if (i_ColSource == i_GameBoard.BoardSize - 1)
                {
                    isCheckIfPawnMostRightCol = false;
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1] != m_Solider.Space)
                    {
                        isCheckIfPawnMostRightCol = false;
                    }
                }
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO)
            {
                if (i_ColSource == 0)
                {
                    isCheckIfPawnMostRightCol = false;
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1] != m_Solider.Space)
                    {
                        isCheckIfPawnMostRightCol = false;
                    }
                }
            }
            else if (IsKing(i_GameBoard, i_RowSource, i_ColSource))
            {
                if (i_ColSource == i_GameBoard.BoardSize - 1 || i_RowSource == 0)
                {
                    if (i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1] != m_Solider.Space)
                    {
                        isCheckIfPawnMostRightCol = false;
                    }
                }
                else if (i_ColSource == 0 || i_RowSource == i_GameBoard.BoardSize - 1)
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1] != m_Solider.Space)
                    {
                        isCheckIfPawnMostRightCol = false;
                    }
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1] != m_Solider.Space && i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1] != m_Solider.Space)
                    {
                        isCheckIfPawnMostRightCol = false;
                    }
                }
            }

            return isCheckIfPawnMostRightCol;
        }

        public bool CheckIfCanMoveFromMostLeftCol(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            bool isCheckIfPawnMostLeftCol = true;

            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX)
            {
                if (i_ColSource == 0)
                {
                    isCheckIfPawnMostLeftCol = false;
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1] == m_Solider.Space)
                    {
                        isCheckIfPawnMostLeftCol = false;
                    }
                }
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO)
            {
                if (i_ColSource == i_GameBoard.BoardSize - 1)
                {
                    isCheckIfPawnMostLeftCol = false;
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1] == m_Solider.Space)
                    {
                        isCheckIfPawnMostLeftCol = false;
                    }
                }
            }
            else if (IsKing(i_GameBoard, i_RowSource, i_ColSource))
            {
                if ((i_RowSource == 0 && i_ColSource == i_GameBoard.BoardSize - 1) || (i_RowSource == i_GameBoard.BoardSize - 1 && i_ColSource == 0))
                {
                    isCheckIfPawnMostLeftCol = false;
                }
                else if (i_ColSource == i_GameBoard.BoardSize - 1 || i_RowSource == i_GameBoard.BoardSize - 1)
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1] == m_Solider.Space)
                    {
                        isCheckIfPawnMostLeftCol = false;
                    }
                }
                else if (i_ColSource == 0 || i_RowSource == 0)
                {
                    if (i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1] == m_Solider.Space)
                    {
                        isCheckIfPawnMostLeftCol = false;
                    }
                }
                else
                {
                    if (i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1] == m_Solider.Space && i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1] == m_Solider.Space)
                    {
                        isCheckIfPawnMostLeftCol = false;
                    }
                }
            }

            return isCheckIfPawnMostLeftCol;
        }

        public bool IsOtherStepAvailable(GameBoard i_GameBoard, out string o_WinningTroop)
        {
            bool isAnyOtherStepAvailable = false;
            string pawn, king;

            if (m_PlayerTurn)
            {
                pawn = m_Solider.SoliderX;
                king = m_Solider.SoldierK;
            }
            else
            {
                pawn = m_Solider.SoliderO;
                king = m_Solider.SoliderU;
            }

            for (int i = 0; i < i_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize; j++)
                {
                    if (i_GameBoard.GetBoard[i, j].Equals(pawn) || i_GameBoard.GetBoard[i, j].Equals(king))
                    {
                        if (CheckIfCanMoveFromMostLeftCol(i_GameBoard, i, j) || CheckIfCanMoveFromMostRightCol(i_GameBoard, i, j))
                        {
                            isAnyOtherStepAvailable = true;
                            break;
                        }
                    }
                }
            }

            if (isAnyOtherStepAvailable)
            {
                o_WinningTroop = " ";
            }
            else
            {
                if (m_PlayerTurn)
                {
                    o_WinningTroop = "O";
                }
                else
                {
                    o_WinningTroop = "X";
                }
            }

            return isAnyOtherStepAvailable;
        }

        public bool HaveJumpedIfNeeded(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool haveJumpedIfNeeded = true;

            if (!IsInputJumpValid(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest))
            {
                if (CheckAnotherJumpsAvailable(i_GameBoard))
                {
                    haveJumpedIfNeeded = false;
                }
            }

            return haveJumpedIfNeeded;
        }

        public bool IsMovementOpponentPawn(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            bool isMovementOppPawn = false;

            if (m_PlayerTurn)
            {
                if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO || i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderU)
                {
                    isMovementOppPawn = true;
                }
            }
            else if (!m_PlayerTurn)
            {
                if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX || i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoldierK)
                {
                    isMovementOppPawn = true;
                }
            }

            return isMovementOppPawn;
        }

        public bool IsQuit(int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool quit = false;

            if (i_RowSource == -1 && i_ColSource == -1 && i_RowDest == -1 && i_ColDest == -1)
            {
                quit = true;
            }

            return quit;
        }

        public bool IsValidMovement(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool isQuit = IsQuit(i_RowSource, i_ColSource, i_RowDest, i_ColDest);
            bool isMovementOpponentPawn;
            bool isValidMovement = true;
            bool isCheckIfSourceHasPawn;
            bool haveJumpedIfNeeded;
            bool isMovementDiagonallyFoward;
            bool isTargetSquerePermitted;

            if (isQuit)
            {
                isValidMovement = true;
            }
            else
            {
                isTargetSquerePermitted = IsTargetEmpty(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
                isMovementOpponentPawn = IsMovementOpponentPawn(i_GameBoard, i_RowSource, i_ColSource);
                isCheckIfSourceHasPawn = CheckIfSourceHasPawn(i_GameBoard, i_RowSource, i_ColSource);
                isMovementDiagonallyFoward = IsMovementForward(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
                haveJumpedIfNeeded = HaveJumpedIfNeeded(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);

                if (!isCheckIfSourceHasPawn || !isMovementDiagonallyFoward || !haveJumpedIfNeeded || !isTargetSquerePermitted || isMovementOpponentPawn)
                {
                    isValidMovement = false;
                }
            }

            return isValidMovement;
        }

        public void EatPawn(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            if (i_RowSource - i_RowDest == 2)
            {
                if (i_ColDest < i_ColSource)
                {
                    i_GameBoard.GetBoard[i_RowSource - 1, i_ColDest + 1] = m_Solider.Space;
                }
                else
                {
                    i_GameBoard.GetBoard[i_RowSource - 1, i_ColDest - 1] = m_Solider.Space;
                }
            }
            else if (i_RowDest - i_RowSource == 2)
            {
                if (i_ColDest < i_ColSource)
                {
                    i_GameBoard.GetBoard[i_RowSource + 1, i_ColDest + 1] = m_Solider.Space;
                }
                else
                {
                    i_GameBoard.GetBoard[i_RowSource + 1, i_ColDest - 1] = m_Solider.Space;
                }
            }
        }

        public void Movement(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderX && i_RowDest == 0)
            {
                i_GameBoard.GetBoard[i_RowDest, i_ColDest] = m_Solider.SoldierK;
            }
            else if (i_GameBoard.GetBoard[i_RowSource, i_ColSource] == m_Solider.SoliderO && i_RowDest == i_GameBoard.BoardSize - 1)
            {
                i_GameBoard.GetBoard[i_RowDest, i_ColDest] = m_Solider.SoliderU;
            }
            else
            {
                i_GameBoard.GetBoard[i_RowDest, i_ColDest] = i_GameBoard.GetBoard[i_RowSource, i_ColSource];
            }

            i_GameBoard.GetBoard[i_RowSource, i_ColSource] = m_Solider.Space;

            if (Math.Abs(i_RowSource - i_RowDest) == 2)
            {
                EatPawn(i_GameBoard, i_RowSource, i_ColSource, i_RowDest, i_ColDest);
            }
        }

        public bool CheckIfDraw(GameBoard i_GameBoard)
        {
            string troop;
            bool isAnyOtherStepAvailable1, isAnyOtherStepAvailable2;

            isAnyOtherStepAvailable1 = IsOtherStepAvailable(i_GameBoard, out troop);
            ChangeTurnOfPlayer();
            isAnyOtherStepAvailable2 = IsOtherStepAvailable(i_GameBoard, out troop);
            ChangeTurnOfPlayer();

            return !isAnyOtherStepAvailable1 && !isAnyOtherStepAvailable2 && !CheckAnotherJumpsAvailable(i_GameBoard);
        }

        public int CountPawn(GameBoard i_GameBoard, string i_Pawn)
        {
            int count = 0;
            string countKing;

            if (i_Pawn == m_Solider.SoliderX)
            {
                countKing = m_Solider.SoldierK;
            }
            else
            {
                countKing = m_Solider.SoliderU;
            }

            for (int i = 0; i < i_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize; j++)
                {
                    if (i_Pawn == i_GameBoard.GetBoard[i, j] || countKing == i_GameBoard.GetBoard[i, j])
                    {
                        if (IsKing(i_GameBoard, i, j))
                        {
                            count += 4;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public bool IsGameOver(GameBoard i_GameBoard, bool i_Quit, out string o_WinningTroop)
        {
            bool isGameOver = true;

            if (i_Quit)
            {
                if (m_PlayerTurn)
                {
                    o_WinningTroop = m_Solider.SoliderO;
                }
                else
                {
                    o_WinningTroop = m_Solider.SoliderX;
                }
            }
            else if (CountPawn(i_GameBoard, m_Solider.SoliderX) == 0 && CountPawn(i_GameBoard, m_Solider.SoldierK) == 0)
            {
                o_WinningTroop = m_Solider.SoliderO;
            }
            else if (CountPawn(i_GameBoard, m_Solider.SoliderO) == 0 && CountPawn(i_GameBoard, m_Solider.SoliderU) == 0)
            {
                o_WinningTroop = m_Solider.SoliderX;
            }
            else if (IsOtherStepAvailable(i_GameBoard, out o_WinningTroop))
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        public bool CheckIfPawnEatValid(string i_SourceTroop, string i_DestinationTroop, string i_FollowingSpot)
        {
            bool isTroopEatLegit = false;

            if (i_SourceTroop == m_Solider.SoliderX || i_SourceTroop == m_Solider.SoldierK)
            {
                isTroopEatLegit = i_FollowingSpot == m_Solider.Space && (i_DestinationTroop == m_Solider.SoliderO || i_DestinationTroop == m_Solider.SoliderU);
            }
            else if (i_SourceTroop == m_Solider.SoliderO || i_SourceTroop == m_Solider.SoliderU)
            {
                isTroopEatLegit = i_FollowingSpot == m_Solider.Space && (i_DestinationTroop == m_Solider.SoliderX || i_DestinationTroop == m_Solider.SoldierK);
            }

            return isTroopEatLegit;
        }

        public bool IsRightJumpPossible(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            string sourcePawn = i_GameBoard.GetBoard[i_RowSource, i_ColSource];
            string destPawn, nextMove;
            bool result = true;

            if (sourcePawn == m_Solider.SoliderX)
            {
                if (i_ColSource >= i_GameBoard.BoardSize - 2 || i_RowSource <= 1)
                {
                    result = false;
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource + 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
            }
            else if (sourcePawn == m_Solider.SoliderO)
            {
                if (i_ColSource <= 1 || i_RowSource >= i_GameBoard.BoardSize - 2)
                {
                    result = false;
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
            }
            else if (sourcePawn == m_Solider.SoldierK || sourcePawn == m_Solider.SoliderU)
            {
                if ((i_ColSource >= i_GameBoard.BoardSize - 2 && i_RowSource >= i_GameBoard.BoardSize - 2) || (i_ColSource <= 1 && i_RowSource <= 1))
                {
                    result = false;
                }
                else if (i_ColSource >= i_GameBoard.BoardSize - 2)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else if (i_RowSource <= 1)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else if (i_ColSource <= 1 || i_RowSource >= i_GameBoard.BoardSize - 2)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource + 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource + 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource + 2];
                    if (!CheckIfPawnEatValid(sourcePawn, destPawn, nextMove))
                    {
                        destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource - 1];
                        nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource - 2];
                        result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                    }
                }
            }

            return result;
        }

        public bool IsLeftJumpPossible(GameBoard i_GameBoard, int i_RowSource, int i_ColSource)
        {
            string sourcePawn = i_GameBoard.GetBoard[i_RowSource, i_ColSource];
            string destPawn, nextMove;
            bool result = true;

            if (sourcePawn == m_Solider.SoliderX)
            {
                if (i_ColSource <= 1 || i_RowSource <= 1)
                {
                    result = false;
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
            }
            else if (sourcePawn == m_Solider.SoliderO)
            {
                if (i_ColSource >= i_GameBoard.BoardSize - 2 || i_RowSource >= i_GameBoard.BoardSize - 2)
                {
                    result = false;
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource + 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
            }
            else if (sourcePawn == m_Solider.SoldierK || sourcePawn == m_Solider.SoliderU)
            {
                if ((i_ColSource <= 1 && i_RowSource >= i_GameBoard.BoardSize - 2) || (i_ColSource >= i_GameBoard.BoardSize - 2 && i_RowSource <= 1))
                {
                    result = false;
                }
                else if (i_ColSource >= i_GameBoard.BoardSize - 2)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else if (i_RowSource <= 1 || i_ColSource <= 1)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource + 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else if (i_RowSource >= i_GameBoard.BoardSize - 2)
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource - 2];
                    result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                }
                else
                {
                    destPawn = i_GameBoard.GetBoard[i_RowSource - 1, i_ColSource - 1];
                    nextMove = i_GameBoard.GetBoard[i_RowSource - 2, i_ColSource - 2];
                    if (!CheckIfPawnEatValid(sourcePawn, destPawn, nextMove))
                    {
                        destPawn = i_GameBoard.GetBoard[i_RowSource + 1, i_ColSource + 1];
                        nextMove = i_GameBoard.GetBoard[i_RowSource + 2, i_ColSource + 2];
                        result = CheckIfPawnEatValid(sourcePawn, destPawn, nextMove);
                    }
                }
            }

            return result;
        }

        public bool IsAdditionalMovement(GameBoard i_GameBoard, int i_RowSource, int i_ColSource, int i_Gap)
        {
            string pawn = i_GameBoard.GetBoard[i_RowSource, i_ColSource];

            return (i_Gap == 2) && (IsRightJumpPossible(i_GameBoard, i_RowSource, i_ColSource) || IsLeftJumpPossible(i_GameBoard, i_RowSource, i_ColSource));
        }

        public bool IsPlayerPawn(int i_PlayerNum, string i_Pawn)
        {
            return (i_PlayerNum == 1 && (i_Pawn == m_Solider.SoliderX || i_Pawn == m_Solider.SoldierK)) || (i_PlayerNum == 2 && (i_Pawn == m_Solider.SoliderO || i_Pawn.Equals(i_Pawn == m_Solider.SoliderU)));
        }

        public string GetLosePawn(string i_WinningPawn)
        {
            string losingPawn = "X";

            if (i_WinningPawn == m_Solider.SoliderX)
            {
                losingPawn = m_Solider.SoliderO;
            }

            return losingPawn;
        }

        public int CalculateResult(GameBoard i_GameBoard, string i_WinningPawn, bool i_Quit)
        {
            int result = 0, winningPawnCounter, losingPawnCounter, deltaResult;

            winningPawnCounter = CountPawn(i_GameBoard, i_WinningPawn);
            losingPawnCounter = CountPawn(i_GameBoard, GetLosePawn(i_WinningPawn));
            deltaResult = winningPawnCounter - losingPawnCounter;
            if (deltaResult == 0 || i_Quit)
            {
                result = winningPawnCounter;
            }
            else
            {
                result = deltaResult;
            }

            return result;
        }

        public void CreatePCMoves(GameBoard i_GameBoard)
        {
            Movement Movement;

            m_MovementsArray = new List<Movement>();
            for (int i = 0; i < i_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.BoardSize; j++)
                {
                    if (i_GameBoard.GetBoard[i, j] == m_Solider.SoliderO || i_GameBoard.GetBoard[i, j] == m_Solider.SoliderU)
                    {
                        if (i + 2 < i_GameBoard.BoardSize && j - 2 >= 0)
                        {
                            if (IsValidMovement(i_GameBoard, i, j, i + 2, j - 2))
                            {
                                Movement = new Movement(i, j, i + 2, j - 2);
                                m_MovementsArray.Add(Movement);
                            }
                        }

                        if (i + 2 < i_GameBoard.BoardSize && j + 2 < i_GameBoard.BoardSize)
                        {
                            if (IsValidMovement(i_GameBoard, i, j, i + 2, j + 2))
                            {
                                Movement = new Movement(i, j, i + 2, j + 2);
                                m_MovementsArray.Add(Movement);
                            }
                        }

                        if (i + 1 < i_GameBoard.BoardSize && j + 1 < i_GameBoard.BoardSize)
                        {
                            if (IsValidMovement(i_GameBoard, i, j, i + 1, j + 1))
                            {
                                Movement = new Movement(i, j, i + 1, j + 1);
                                m_MovementsArray.Add(Movement);
                            }
                        }

                        if (i + 1 < i_GameBoard.BoardSize && j - 1 >= 0)
                        {
                            if (IsValidMovement(i_GameBoard, i, j, i + 1, j - 1))
                            {
                                Movement = new Movement(i, j, i + 1, j - 1);
                                m_MovementsArray.Add(Movement);
                            }
                        }

                        if (i_GameBoard.GetBoard[i, j].Equals("U"))
                        {
                            if (i - 2 >= 0 && j - 2 >= 0)
                            {
                                if (IsValidMovement(i_GameBoard, i, j, i - 2, j - 2))
                                {
                                    Movement = new Movement(i, j, i - 2, j - 2);
                                    m_MovementsArray.Add(Movement);
                                }
                            }

                            if (i - 2 >= 0 && j + 2 < i_GameBoard.BoardSize)
                            {
                                if (IsValidMovement(i_GameBoard, i, j, i - 2, j + 2))
                                {
                                    Movement = new Movement(i, j, i - 2, j + 2);
                                    m_MovementsArray.Add(Movement);
                                }
                            }

                            if (i - 1 >= 0 && j - 1 >= 0)
                            {
                                if (IsValidMovement(i_GameBoard, i, j, i - 1, j - 1))
                                {
                                    Movement = new Movement(i, j, i - 1, j - 1);
                                    m_MovementsArray.Add(Movement);
                                }
                            }

                            if (i - 1 >= 0 && j + 1 < i_GameBoard.BoardSize)
                            {
                                if (IsValidMovement(i_GameBoard, i, j, i - 1, j + 1))
                                {
                                    Movement = new Movement(i, j, i - 1, j + 1);
                                    m_MovementsArray.Add(Movement);
                                }
                            }
                        }
                    }
                }
            }
        }

        public Movement GetComputerMovement(GameBoard i_GameBoard)
        {
            int index;
            Random random = new Random();

            CreatePCMoves(i_GameBoard);
            index = random.Next(0, m_MovementsArray.Count);

            return m_MovementsArray[index];
        }

        public bool IsComputerHasRemainingMovements(GameBoard i_GameBoard)
        {
            string pawn;
            return CheckAnotherJumpsAvailable(i_GameBoard) || IsOtherStepAvailable(i_GameBoard, out pawn);
        }

        public class Soldier
        {
            private const string m_soliderSymbolO = "O";
            private const string m_soliderSymbolX = "X";
            private const string m_soliderSymbolU = "U";
            private const string m_soliderSymbolK = "K";
            private const string m_space = " ";

            public string SoliderO
            {
                get { return m_soliderSymbolO; }
            }

            public string SoliderX
            {
                get { return m_soliderSymbolX; }
            }

            public string Space
            {
                get { return m_space; }
            }

            public string SoliderU
            {
                get { return m_soliderSymbolU; }
            }

            public string SoldierK
            {
                get { return m_soliderSymbolK; }
            }
        }
    }
}
