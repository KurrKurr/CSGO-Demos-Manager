﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace CSGO_Demos_Manager.Models
{
	public class TeamExtended : ObservableObject
	{
		private string _name;

		[JsonProperty("team_name")]
		public string Name
		{
			get { return _name; }
			set { Set(() => Name, ref _name, value); }
		}

		[JsonProperty("team_players", IsReference = false)]
		public ObservableCollection<PlayerExtended> Players { get; set; }

		[JsonIgnore]
		public int EntryKillCount
		{
			get { return Players.SelectMany(p => p.EntryKills).Count(); }
		}

		[JsonIgnore]
		public int EntryKillWinCount
		{
			get { return Players.SelectMany(p => p.EntryKills).Count(e => e.HasWin); }
		}

		[JsonIgnore]
		public int EntryKillLossCount
		{
			get { return Players.SelectMany(p => p.EntryKills).Count(e => e.HasWin == false); }
		}

		[JsonIgnore]
		public int FlashbangThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.FlashbangThrownCount);

		[JsonIgnore]
		public int HeGrenadeThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.HeGrenadeThrownCount);

		[JsonIgnore]
		public int SmokeThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.SmokeThrownCount);

		[JsonIgnore]
		public int MolotovThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.MolotovThrownCount);

		[JsonIgnore]
		public int IncendiaryThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.IncendiaryThrownCount);

		[JsonIgnore]
		public int DecoyThrownCount => Players.Where(playerExtended => playerExtended.TeamName == Name)
			.Sum(playerExtended => playerExtended.DecoyThrownCount);

		[JsonIgnore]
		public int TradeKillCount => Players.Sum(p => p.TradeKillCount);

		[JsonIgnore]
		public int TradeDeathCount => Players.Sum(p => p.TradeDeathCount);

		[JsonIgnore]
		public int OpenKillCount
		{
			get { return Players.SelectMany(p => p.OpeningKills).Count(); }
		}

		[JsonIgnore]
		public int OpenKillWinCount
		{
			get { return Players.SelectMany(p => p.OpeningKills).Count(e => e.HasWin); }
		}

		[JsonIgnore]
		public int OpenKillLossCount
		{
			get { return Players.SelectMany(p => p.OpeningKills).Count(e => e.HasWin == false); }
		}

		[JsonIgnore]
		public decimal RatioEntryKill
		{
			get
			{
				int entryKillCount = Players.SelectMany(p => p.EntryKills).Count();
				int entryKillWin = Players.SelectMany(p => p.EntryKills).Count(e => e.HasWin);
				int entryKillLoss = Players.SelectMany(p => p.EntryKills).Count(e => e.HasWin == false);

				decimal entryKillPercent = 0;
				if (EntryKillWinCount == 0) return entryKillPercent;
				if (entryKillLoss == 0) return 100;
				entryKillPercent = (entryKillWin / (decimal)entryKillCount) * 100;
				entryKillPercent = Math.Round(entryKillPercent, 0);

				return entryKillPercent;
			}
		}

		[JsonIgnore]
		public string RatioEntryKillAsString => RatioEntryKill + " %";

		[JsonIgnore]
		public decimal RatioOpenKill
		{
			get
			{
				int openKillCount = Players.SelectMany(p => p.OpeningKills).Count();
				int openKillWin = Players.SelectMany(p => p.OpeningKills).Count(e => e.HasWin);
				int openKillLoss = Players.SelectMany(p => p.OpeningKills).Count(e => e.HasWin == false);

				decimal openKillPercent = 0;
				if (openKillWin == 0) return openKillPercent;
				if (openKillLoss == 0) return 100;
				openKillPercent = (openKillWin / (decimal)openKillCount) * 100;
				openKillPercent = Math.Round(openKillPercent, 0);

				return openKillPercent;
			}
		}

		[JsonIgnore]
		public string RatioOpenKillAsString => RatioOpenKill + " %";

		[JsonIgnore]
		public int MatchCount { get; set; } = 1;

		[JsonIgnore]
		public int WinCount { get; set; } = 0;

		[JsonIgnore]
		public int LostCount { get; set; } = 0;

		[JsonIgnore]
		public int KillCount => Players.Sum(player => player.KillsCount);

		[JsonIgnore]
		public int AssistCount => Players.Sum(player => player.AssistCount);

		[JsonIgnore]
		public int DeathCount => Players.Sum(player => player.DeathCount);

		[JsonIgnore]
		public int RoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundCtCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundCtCount { get; set; } = 0;

		[JsonIgnore]
		public int WinRoundTCount { get; set; } = 0;

		[JsonIgnore]
		public int LostRoundTCount { get; set; } = 0;

		[JsonIgnore]
		public int WinPistolRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinEcoRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinSemiEcoRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int WinForceBuyRoundCount { get; set; } = 0;

		[JsonIgnore]
		public int BombPlantedCount => Players.Sum(player => player.BombPlantedCount);

		[JsonIgnore]
		public int BombDefusedCount => Players.Sum(player => player.BombDefusedCount);

		[JsonIgnore]
		public int BombExplodedCount => Players.Sum(player => player.BombExplodedCount);

		[JsonIgnore]
		public int BombPlantedOnACount { get; set; } = 0;

		[JsonIgnore]
		public int BombPlantedOnBCount { get; set; } = 0;

		[JsonIgnore]
		public int FiveKillCount => Players.Sum(player => player.FiveKillCount);

		[JsonIgnore]
		public int FourKillCount => Players.Sum(player => player.FourKillCount);

		[JsonIgnore]
		public int ThreeKillCount => Players.Sum(player => player.ThreekillCount);

		[JsonIgnore]
		public int TwoKillCount => Players.Sum(player => player.TwokillCount);

		[JsonIgnore]
		public int OneKillCount => Players.Sum(player => player.OnekillCount);

		[JsonIgnore]
		public int JumpKillCount => Players.Sum(player => player.JumpKillCount);

		[JsonIgnore]
		public int CrouchKillCount => Players.Sum(player => player.CrouchKillCount);

		public override bool Equals(object obj)
		{
			var item = obj as TeamExtended;

			return item != null
				&& string.Compare(Name, item.Name, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public TeamExtended()
		{
			Players = new ObservableCollection<PlayerExtended>();
			Players.CollectionChanged += OnPlayersCollectionChanged;
		}

		public void Clear()
		{
			Players.Clear();
		}

		public TeamExtended Clone()
		{
			return (TeamExtended)MemberwiseClone();
		}

		private void OnPlayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => Players);
			if (Players.Any())
			{
				foreach (var player in Players)
				{
					if (player != null)
					{
						player.EntryKills.CollectionChanged += OnEntryKillsCollectionChanged;
						player.OpeningKills.CollectionChanged += OnOpenKillsCollectionChanged;
					}
				}
			}
		}

		private void OnEntryKillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => EntryKillCount);
			RaisePropertyChanged(() => EntryKillWinCount);
			RaisePropertyChanged(() => EntryKillLossCount);
			RaisePropertyChanged(() => RatioEntryKillAsString);
		}

		private void OnOpenKillsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(() => OpenKillCount);
			RaisePropertyChanged(() => OpenKillWinCount);
			RaisePropertyChanged(() => OpenKillLossCount);
			RaisePropertyChanged(() => RatioOpenKillAsString);
		}
	}
}