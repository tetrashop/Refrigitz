/**************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
*************TETRASHOP.IR**************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
**************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Chess;
using System.Diagnostics;
using RefrigtzDLL;

namespace Refrigtz
{
    [Serializable]
    public class TakeRoot
    {
        
        readonly String path3 = @"temp";
        String AllDrawReplacement = "";
        public static int AllDrawKind = 0;
        public static String AllDrawKindString = "";
        public RefrigtzDLL.AllDraw t = null;
        
        static void Log(Exception ex)
        {
            try
            {
                Object a = new Object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    File.AppendAllText(RefrigtzDLLForm.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); 
                }
            }
            catch (Exception t) { Log(t); }
        }
        void SetAllDrawKindString()
        {
            Object OO = new Object();
            lock (OO)
            {
                if (AllDrawKind == 4)
                    AllDrawKindString = "AllDrawBT.asd";
                else
                if (AllDrawKind == 3)
                    AllDrawKindString = "AllDrawFFST.asd";
                else
                if (AllDrawKind == 2)
                    AllDrawKindString = "AllDrawFTSF.asd";
                else
                if (AllDrawKind == 1)
                    AllDrawKindString = "AllDrawFFSF.asd";

            }
        }
        void SetAllDrawKind(bool UsePenaltyRegardMechnisam, bool AStarGreedyHeuristic)
        {
            Object OO = new Object();
            lock (OO)
            {
                if (UsePenaltyRegardMechnisam && AStarGreedyHeuristic)
                    AllDrawKind = 4;
                else
          if ((!UsePenaltyRegardMechnisam) && AStarGreedyHeuristic)
                    AllDrawKind = 3;
                if (UsePenaltyRegardMechnisam && (!AStarGreedyHeuristic))
                    AllDrawKind = 2;
                if ((!UsePenaltyRegardMechnisam) && (!AStarGreedyHeuristic))
                    AllDrawKind = 1;
            }
        }
        bool DrawManagement(bool FOUND,bool UsePenaltyRegardMechnisam, bool AStarGreedyHeuristic)
        {
            Object OO = new Object();
            lock (OO)
            {
                SetAllDrawKind(UsePenaltyRegardMechnisam, AStarGreedyHeuristic);
                //Set Configuration To True for some unknown reason!.
                
                SetAllDrawKindString();

                bool Found = false;
                String P = Path.GetFullPath(path3);
                AllDrawReplacement = Path.Combine(P, AllDrawKindString);
                Logger y = new Logger(AllDrawReplacement);
                
                y = new Logger(AllDrawKindString);

                if (File.Exists(AllDrawReplacement))
                {
                    if (AllDraw.HarasAct)
                        File.Delete(AllDrawReplacement);
                }
                if (File.Exists(AllDrawKindString))
                {
                    if (AllDraw.HarasAct)
                        File.Delete(AllDrawKindString);

                }
                AllDraw.HarasAct = false;
                if (File.Exists(AllDrawKindString))
                {
                    if (File.Exists(AllDrawReplacement))
                    {
                        if (((new System.IO.FileInfo(AllDrawKindString).Length) < (new System.IO.FileInfo(AllDrawReplacement)).Length))
                        {
                            File.Delete(AllDrawKindString);
                            File.Copy(AllDrawReplacement, AllDrawKindString);
                            Found = true;
                        }
                        else if (((new System.IO.FileInfo(AllDrawKindString).Length) > (new System.IO.FileInfo(AllDrawReplacement)).Length))
                        {
                            if (File.Exists(AllDrawReplacement))
                                File.Delete(AllDrawReplacement);
                            File.Copy(AllDrawKindString, AllDrawReplacement);
                            Found = true;
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(Path.GetFullPath(path3)))
                            Directory.CreateDirectory(Path.GetFullPath(path3));
                        File.Copy(AllDrawKindString, AllDrawReplacement);
                        Found = true;
                    }
                    Found = true;
                }
                else if (File.Exists(AllDrawReplacement))
                {
                    File.Copy(AllDrawReplacement, AllDrawKindString);
                    Found = true;
                }
                return Found;
            }
        }
        public bool Load(bool FOUND, bool Quantum, RefrigtzDLLForm Curent, ref bool LoadTree, bool MovementsAStarGreedyHeuristicFound, bool IInoreSelfObjects, bool UsePenaltyRegardMechnisam, bool BestMovments, bool PredictHeuristic, bool OnlySelf, bool AStarGreedyHeuristic, bool ArrangmentsChanged)
        {
            Object OO = new Object();
            lock (OO)
            {
                DrawManagement(FOUND, UsePenaltyRegardMechnisam, AStarGreedyHeuristic);
                bool DrawDrawen = false;
                //Load Middle Targets.
                try
                {
                    if (File.Exists(RefrigtzDLLForm.AllDrawKindString))
                    {
                        if (RefrigtzDLLForm.MovmentsNumber >= 0)
                        {
                            //if (!Quantum)
                            {
                                GalleryStudio.RefregizMemmory tr = new GalleryStudio.RefregizMemmory(MovementsAStarGreedyHeuristicFound, IInoreSelfObjects, UsePenaltyRegardMechnisam, BestMovments, PredictHeuristic, OnlySelf, AStarGreedyHeuristic, ArrangmentsChanged);
                                t = (RefrigtzDLL.AllDraw)tr.Load(Quantum, RefrigtzDLLForm.OrderPlate);
                                if (t != null)
                                {
                                    Curent.Draw = t;
                                    LoadTree = true;
                                    Curent.Draw = Curent.RootFound();
                                    //Curent.Draw.UpdateLoseAndWinDepenOfKind(Curent.Draw.OrderP);

                                    t = Curent.Draw;
                                    
                                    DrawDrawen = true;
                                    System.Windows.Forms.MessageBox.Show("Load Completed.");
                                }
                            }
                            
                        }
                        File.Delete(RefrigtzDLLForm.AllDrawKindString);
                    }
                }
                catch (Exception t) { Log(t); }
                
                
                
                return DrawDrawen;
            }
        }
        void Wait()
        {
            Object O = new Object();
            lock (O)
            {
                PerformanceCounter myAppCpu =
                    new PerformanceCounter(
                        "Process", "% Processor Time", Process.GetCurrentProcess().ProcessName, true);

                
                
            }
        }
        public bool Save(bool FOUND,bool Quantum, RefrigtzDLLForm Curent, ref bool LoadTree, bool MovementsAStarGreedyHeuristicFound, bool IInoreSelfObjects, bool UsePenaltyRegardMechnisam, bool BestMovments, bool PredictHeuristic, bool OnlySelf, bool AStarGreedyHeuristic, bool ArrangmentsChanged)
        {
            Object OO = new Object();
            lock (OO)
            {


                object o = new object();
                lock (o)
                {
                    if (!Quantum)
                    {
                        if (!RefrigtzDLL.AllDraw.ChangedInTreeOccured)
                            return true;
                    }
                }

                try
                {

                    RefrigtzDLL.AllDraw Stote = Curent.Draw;
                    if (!File.Exists(AllDrawKindString))
                    {
                        GalleryStudio.RefregizMemmory rt = new GalleryStudio.RefregizMemmory(MovementsAStarGreedyHeuristicFound, IInoreSelfObjects, UsePenaltyRegardMechnisam, BestMovments, PredictHeuristic, OnlySelf, AStarGreedyHeuristic, ArrangmentsChanged
                            );
                        //if (!Quantum)
                        {
                            if (Curent.Draw != null)
                            {
                                Curent.Draw = Curent.RootFound();
                                rt.AllDrawCurrentAccess = Curent.Draw;
                                rt.RewriteAllDraw(RefrigtzDLLForm.OrderPlate);
                                RefrigtzDLL.AllDraw.DrawTable = false;
                                
                                
                                
                                
                                
                                
                            }
                        }
                        
                    }
                    else
                          if (File.Exists(AllDrawKindString))
                    {
                        
                        File.Delete(RefrigtzDLLForm.AllDrawKindString);
                        GalleryStudio.RefregizMemmory rt = new GalleryStudio.RefregizMemmory(MovementsAStarGreedyHeuristicFound, IInoreSelfObjects, UsePenaltyRegardMechnisam, BestMovments, PredictHeuristic, OnlySelf, AStarGreedyHeuristic, ArrangmentsChanged
                            );
                        
                        if (Curent.Draw != null)
                        {
                            Curent.Draw = Curent.RootFound();
                            rt.AllDrawCurrentAccess = Curent.Draw;
                            rt.RewriteAllDraw(RefrigtzDLLForm.OrderPlate);
                            RefrigtzDLL.AllDraw.DrawTable = false;
                            
                            
                            
                            
                            
                            
                        }
                        
                    }
                    Curent.Draw = Stote;
                    return true;
                    return true;
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
        }
    }
}
