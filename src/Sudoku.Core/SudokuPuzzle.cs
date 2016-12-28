using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[assembly: InternalsVisibleTo("Sudoku.Tests")]

namespace Sudoku
{
    [Serializable]
    public sealed class SudokuPuzzle : ICloneable
    {
        #region Internal Static Data

        private const int Size  = 9;
        private const int Slice = 3;

        #endregion

        #region Internal Data

        private SudokuPiece[,] board = new SudokuPiece[Size,Size];
        
        #endregion

        #region .Ctor

        internal SudokuPuzzle(SudokuPiece[,] board)
        {
            this.board = board;
        }

        #endregion

        #region Properties

        public int RemainingPieces
        {
            get
            {
                int counter = 0;
                for (int i = 0; i < Size; ++i)
                {
                    for (int k = 0; k < Size; ++k)
                    {
                        if(board[i, k].Value == 0)
                        {
                            counter++;
                        }
                    }
                }
                return counter;
            }
        }

        #endregion

        #region Helper Methods

        internal SudokuPiece[] GetRow(int row)
        {
            var result = new SudokuPiece[Size];
            for (int i = 0; i < Size; i++)
            {
                result[i] = board[row, i];
            }
            return result;
        }

        internal SudokuPiece[] GetColumn(int column)
        {
            var result = new SudokuPiece[Size];
            for (int i = 0; i < Size; i++)
            {
                result[i] = board[i, column];
            }
            return result;
        }

        private SudokuPiece[,] GetArea(int x, int y, int size)
        {
            var result = new SudokuPiece[size,size];

            int deltaX = size + x;
            int deltaY = size + y;

            int ci = 0;
            for (int i = x; i < deltaX; ++i, ++ci)
            {
                int ck = 0;
                for (int k = y; k < deltaY; ++k, ++ck)
                {
                    result[ci, ck] = board[i, k];
                }
            }

            return result;
        }

        internal SudokuPiece[,] GetArea(int index)
        {
            int x, y;
            GetAreaCoordinates(index, out x, out y);
            return GetArea(y, x, Slice);
        }

        private static int GetAreaIndex(int x, int y)
        {
            int nX = x/Slice;
            int nY = y/Slice;

            return Slice*nY + nX;
        }

        private static void GetAreaCoordinates(int index, out int x, out int y)
        {
            switch (index)
            {
                case 0: x = 0; y = 0; return;
                case 1: x = 3; y = 0; return;
                case 2: x = 6; y = 0; return;
                case 3: x = 0; y = 3; return;
                case 4: x = 3; y = 3; return;
                case 5: x = 6; y = 3; return;
                case 6: x = 0; y = 6; return;
                case 7: x = 3; y = 6; return;
                case 8: x = 6; y = 6; return;
            }

            throw new Exception();

        }

        private void AssignCell(int i, int k, int value)
        {
            board[i, k].Value = value;
            board[i, k].PossibleValues = null;
        }

        #endregion

        #region Pencilmarks

        private bool AddPencilmarks()
        {
            bool isComplete = true;

            for (int i = 0; i < Size; ++i)
            {
                for (int k = 0; k < Size; ++k)
                {
                    int value = board[i, k].Value;

                    if (value != 0)
                    {
                        continue;
                    }

                    isComplete = false;
                    IList<int> pencilMarks = GetPencilmarks(GetRow(i), GetColumn(k), GetArea(GetAreaIndex(k, i)));

                    if (pencilMarks.Count == 1)
                    {
                        AssignCell(i, k, pencilMarks[0]);
                        return AddPencilmarks();
                    }

                    board[i, k].PossibleValues = pencilMarks;

                }
            }

            return isComplete;

        }

        private static IList<int> GetPencilmarks(SudokuPiece[] a, SudokuPiece[] b, SudokuPiece[,] c)
        {
            Dictionary<int, int> set = new Dictionary<int, int>();

            AddToDictionary(a, set);
            AddToDictionary(b, set);
            AddToDictionary(c, set);

            IList<int> result = new List<int>();

            for (int i = 1; i <= Size; ++i)
            {
                if (set.ContainsKey(i) == false)
                {
                    result.Add(i);
                }
            }

            return result;

        }

        private static void AddToDictionary(SudokuPiece[] source, IDictionary<int, int> set)
        {
            int count = source.Length;
            for (int i = 0; i < count; ++i)
            {
                int value = source[i].Value;
                if (value != 0 && set.ContainsKey(value) == false)
                {
                    set[value] = value;
                }
            }
        }

        private static void AddToDictionary(SudokuPiece[,] source, IDictionary<int, int> set)
        {
            for (int i = 0; i < Slice; ++i)
            {
                for (int k = 0; k < Slice; ++k)
                {
                    int value = source[i, k].Value;
                    if (value != 0 && set.ContainsKey(value) == false)
                    {
                        set[value] = value;
                    }
                }
            }
        }

        #endregion

        #region SingleCandidates

        private bool SingleCandidates()
        {
            for (int i = 0; i < Size; ++i)
            {
                if (SingleCandidatesInRow(i) || SingleCandidatesInColumn(i) || SingleCandidatesInArea(i))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SingleCandidatesInRow(int index)
        {
            IDictionary<int, CandidateHelper> unique = UniquePencilmarks(GetRow(index));

            if(unique.Count == 0)
            {
                return false;
            }

            foreach (var item in unique)
            {
                int key = item.Key;
                CandidateHelper value = unique[key];
                AssignCell(index, value.Coordinates.X, key);
            }

            return true;

        }

        private bool SingleCandidatesInColumn(int index)
        {
            IDictionary<int, CandidateHelper> unique = UniquePencilmarks(GetColumn(index));

            if (unique.Count == 0)
            {
                return false;
            }

            foreach (var item in unique)
            {
                int key = item.Key;
                CandidateHelper value = unique[key];
                AssignCell(value.Coordinates.X, index, key);
            }

            return true;
        }

        private bool SingleCandidatesInArea(int index)
        {
            IDictionary<int, CandidateHelper> unique = UniquePencilmarks(GetArea(index));

            if (unique.Count == 0)
            {
                return false;
            }

            int deltaX;
            int deltaY;

            GetAreaCoordinates(index, out deltaX, out deltaY);

            foreach (var item in unique)
            {
                int key = item.Key;
                CandidateHelper value = unique[key];
                AssignCell(deltaY + value.Coordinates.Y, deltaX + value.Coordinates.X, key);
            }

            return true;
        }

        private static IDictionary<int, CandidateHelper> UniquePencilmarks(IList<SudokuPiece> p)
        {
            Dictionary<int, CandidateHelper> result = new Dictionary<int, CandidateHelper>();

            for (int i = 0; i < p.Count; ++i)
            {
                SudokuPiece sp = p[i];
                if (sp.Value != 0 || sp.PossibleValues == null || sp.PossibleValues.Count == 0) continue;
                int count = sp.PossibleValues.Count;
                for (int k = 0; k < count; ++k)
                {
                    int keyValue = sp.PossibleValues[k];
                    result[keyValue] = result.ContainsKey(keyValue) == false ? new CandidateHelper(1, i) : new CandidateHelper(result[keyValue]);
                }
            }

            return FilterUniques(result);
        }

        private static IDictionary<int, CandidateHelper> UniquePencilmarks(SudokuPiece[,] p)
        {
            Dictionary<int, CandidateHelper> result = new Dictionary<int, CandidateHelper>();

            for (int i = 0; i < Slice; ++i)
            {
                for (int k = 0; k < Slice; ++k)
                {
                    SudokuPiece sp = p[k, i];
                    if (sp.Value != 0 || sp.PossibleValues == null || sp.PossibleValues.Count == 0)
                    {
                        continue;
                    }
                    for (int w = 0; w < sp.PossibleValues.Count; ++w)
                    {
                        int keyValue = sp.PossibleValues[w];
                        result[keyValue] = result.ContainsKey(keyValue) == false ? new CandidateHelper(1, i, k) : new CandidateHelper(result[keyValue]);
                    }
                }
            }

            return FilterUniques(result);
        }

        private static IDictionary<int, CandidateHelper> FilterUniques(IDictionary<int, CandidateHelper> c)
        {
            Dictionary<int, CandidateHelper> unique = new Dictionary<int, CandidateHelper>();

            foreach (var kvp in c)
            {
                int key = kvp.Key;
                CandidateHelper value = c[key];

                if (value.Count == 1)
                {
                    unique.Add(key, value);
                }

            }

            return unique;
        }

        #endregion

        #region CandidateLines

        private bool CandidateLines()
        {
            bool found = false;
            for (int i = 0; i < Size; ++i)
            {
                found = ProcessCandidates(i, GetArea(i)) || found;
            }
            return found;
        }

        private bool ProcessCandidates(int index, SudokuPiece[,] area)
        {
            GetAreaCoordinates(index, out int x, out int y);

            IDictionary<int, IList<Point>> candidates = GetCandidates(area, x, y);

            IDictionary<int, int> fx = new Dictionary<int, int>();
            IDictionary<int, int> fy = new Dictionary<int, int>();

            FilterCandidates(candidates, fx, fy);

            return UpdateCandidates(fx, fy, x, y);
        }

        private bool UpdateCandidates(IDictionary<int, int> fx, IDictionary<int, int> fy, int x, int y)
        {
            if (fx.Count == 0 && fy.Count == 0)
            {
                return false;
            }

            bool changed = false;

            foreach (var item in fx)
            {
                for (int i = 0; i < x; i++)
                {
                    changed = UpdateCandidatesOnX(i, item) || changed;
                }
                for (int i = x + Slice; i < Size; i++)
                {
                    changed = UpdateCandidatesOnX(i, item) || changed;
                }
            }

            foreach (var item in fy)
            {
                for (int i = 0; i < y; i++)
                {
                    changed = UpdateCandidatesOnY(i, item) || changed;
                }
                for (int i = y + Slice; i < Size; i++)
                {
                    changed = UpdateCandidatesOnY(i, item) || changed;
                }
            }

            return changed;
        }

        private static IDictionary<int, IList<Point>> GetCandidates(SudokuPiece[,] area, int deltaX, int deltaY)
        {
            IDictionary<int, IList<Point>> candidates = new Dictionary<int, IList<Point>>();

            for (int i = 0; i < Slice; ++i)
            {
                for (int k = 0; k < Slice; ++k)
                {
                    SudokuPiece temp = area[i, k];
                    if (temp.Value != 0) { continue; }
                    IList<int> pv = temp.PossibleValues;
                    for (int w = 0; w < pv.Count; ++w)
                    {
                        int value = pv[w];
                        if (candidates.ContainsKey(value) == false)
                        {
                            candidates[value] = new List<Point>();
                        }
                        candidates[value].Add(new Point(k + deltaX, i + deltaY));
                    }
                }
            }
            return candidates;
        }

        private static void FilterCandidates(IDictionary<int, IList<Point>> c, IDictionary<int, int> fx, IDictionary<int, int> fy)
        {
            foreach (var item in c)
            {
                bool addY = true;
                bool addX = true;
                int count = item.Value.Count - 1;

                for (int i = 0; i < count; ++i)
                {
                    Point pc = item.Value[i];
                    Point pn = item.Value[i + 1];

                    if (pc.X != pn.X)
                    {
                        addY = false;
                    }

                    if (pc.Y != pn.Y)
                    {
                        addX = false;
                    }

                    if (addY == false && addX == false)
                    {
                        break;
                    }
                }

                if (addY)
                {
                    fy[item.Key] = item.Value[0].X;
                }

                if (addX)
                {
                    fx[item.Key] = item.Value[0].Y;
                }
            }
        }

        private bool UpdateCandidatesOnX(int i, KeyValuePair<int, int> item)
        {
            SudokuPiece sp = board[item.Value, i];

            if (sp.Value != 0)
            {
                return false;
            }
            
            sp.PossibleValues.Remove(item.Key);

            if (sp.PossibleValues.Count == 1)
            {
                AssignCell(item.Value, i, sp.PossibleValues[0]);
                return true;
            }

            return false;
        }

        private bool UpdateCandidatesOnY(int i, KeyValuePair<int, int> item)
        {
            SudokuPiece sp = board[i, item.Value];
            if (sp.Value != 0) { return false; }

            sp.PossibleValues.Remove(item.Key);
            
            if (sp.PossibleValues.Count == 1)
            {
                AssignCell(i, item.Value, sp.PossibleValues[0]);
                return true;
            }

            return false;
        }

        #endregion

        #region Solve

        public void Solve()
        {
            InternalSolve();

            if (RemainingPieces != 0)
            {
                BruteForce();
            }
        }

        private void InternalSolve()
        {
            if (AddPencilmarks())
            {
                return;
            }

            if (SingleCandidates())
            {
                InternalSolve();
                return;
            }

            if (CandidateLines())
            {
                InternalSolve();
                return;
            }
        }

        #endregion

        #region Brute Force

        private void BruteForce()
        {
            for (int i = 0; i < Size; ++i)
            {
                for (int k = 0; k < Size; ++k)
                {
                    SudokuPiece sp = board[i, k];
                    if (sp.Value != 0 || sp.PossibleValues.Count != 2) { continue; }

                    var clone1 = (SudokuPuzzle)this.Clone();
                    clone1.board[i, k] = new SudokuPiece(sp.PossibleValues[0]);
                    clone1.Solve();
                    if (clone1.RemainingPieces == 0)
                    {
                        this.board = clone1.board;
                    }
                    else
                    {
                        var clone2 = (SudokuPuzzle)this.Clone();
                        clone2.board[i, k] = new SudokuPiece(sp.PossibleValues[1]);
                        clone2.Solve();
                        if (clone2.RemainingPieces == 0)
                        {
                            this.board = clone2.board;
                        }
                    }
                }
            }
        }

        #endregion

        #region Base Methods

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Size; ++i)
            {
                if ((i % 3) == 0)
                {
                    sb.Append(new string('_', 19));
                    sb.AppendLine();
                }

                sb.Append("|");

                for (int k = 0; k < Size; ++k)
                {
                    if (k != 0 && (k % 3) == 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("|");
                    }

                    int value = board[i, k].Value;

                    if(value == 0)
                    {
                        sb.Append("_ ");
                    }
                    else
                    {
                        sb.Append(value + new string(' ', 2 - value.ToString().Length));
                    }
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("|");
                sb.AppendLine();
            }

            sb.Append(new string('_', 19));

            return sb.ToString();
        }

        public object Clone()
        {
            using(MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                return bf.Deserialize(ms);
            }
        }

        #endregion

        #region Other

        public SudokuPiece[,] GetPuzzle()
        {
            var clone = (SudokuPuzzle) Clone();
            return clone.board;
        }

        #endregion
    }
}