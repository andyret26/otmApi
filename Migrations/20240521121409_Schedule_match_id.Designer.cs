﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OtmApi.Data;

#nullable disable

namespace otmApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240521121409_Schedule_match_id")]
    partial class Schedule_match_id
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OtmApi.Data.Entities.Host", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar_url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country_code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DiscordUsername")
                        .HasColumnType("text");

                    b.Property<int>("Global_rank")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.QualsSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("MatchId")
                        .HasColumnType("integer");

                    b.Property<bool>("MpLinkIsVisable")
                        .HasColumnType("boolean");

                    b.Property<List<string>>("Names")
                        .HasColumnType("text[]");

                    b.Property<string>("Num")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Referee")
                        .HasColumnType("text");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("QualsSchedules");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsQualifier")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<List<string>>("Commentators")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("MatchId")
                        .HasColumnType("integer");

                    b.Property<string>("Name1")
                        .HasColumnType("text");

                    b.Property<string>("Name2")
                        .HasColumnType("text");

                    b.Property<string>("Referee")
                        .HasColumnType("text");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<string>("Streamer")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Staff", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Roles")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "TournamentId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Stats", b =>
                {
                    b.Property<int>("MapId")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Acc")
                        .HasColumnType("numeric");

                    b.Property<string>("MapMod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Mods")
                        .HasColumnType("text[]");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("MapId", "PlayerId", "RoundId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.HasIndex("MapId", "MapMod");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMap", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Mod")
                        .HasColumnType("text");

                    b.Property<decimal>("Accuracy")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Ar")
                        .HasColumnType("numeric");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Bpm")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Cs")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Difficulty_rating")
                        .HasColumnType("numeric");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Mapper")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total_length")
                        .HasColumnType("numeric");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "Mod");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMapSuggestion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Mod")
                        .HasColumnType("text");

                    b.Property<decimal>("Accuracy")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Ar")
                        .HasColumnType("numeric");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Bpm")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Cs")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Difficulty_rating")
                        .HasColumnType("numeric");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Mapper")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total_length")
                        .HasColumnType("numeric");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "Mod");

                    b.ToTable("MapSuggestions");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Isknockout")
                        .HasColumnType("boolean");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FormuPostLink")
                        .HasColumnType("text");

                    b.Property<int>("HostId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsTeamTourney")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxTeamSize")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RankRange")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TournamentPlayer", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<bool>("Isknockout")
                        .HasColumnType("boolean");

                    b.HasKey("PlayerId", "TournamentId");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentPlayer");
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamsId")
                        .HasColumnType("integer");

                    b.HasKey("PlayersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("TeamPlayer", (string)null);
                });

            modelBuilder.Entity("RoundTMap", b =>
                {
                    b.Property<int>("RoundsId")
                        .HasColumnType("integer");

                    b.Property<int>("MappoolId")
                        .HasColumnType("integer");

                    b.Property<string>("MappoolMod")
                        .HasColumnType("text");

                    b.HasKey("RoundsId", "MappoolId", "MappoolMod");

                    b.HasIndex("MappoolId", "MappoolMod");

                    b.ToTable("RoundMap", (string)null);
                });

            modelBuilder.Entity("RoundTMapSuggestion", b =>
                {
                    b.Property<int>("RoundsId")
                        .HasColumnType("integer");

                    b.Property<int>("MapSuggestionsId")
                        .HasColumnType("integer");

                    b.Property<string>("MapSuggestionsMod")
                        .HasColumnType("text");

                    b.HasKey("RoundsId", "MapSuggestionsId", "MapSuggestionsMod");

                    b.HasIndex("MapSuggestionsId", "MapSuggestionsMod");

                    b.ToTable("RoundMapSuggestion", (string)null);
                });

            modelBuilder.Entity("OtmApi.Data.Entities.QualsSchedule", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Round");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Round", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Tournament", "Tournament")
                        .WithMany("Rounds")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Schedule", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Round");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Staff", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Tournament", "Tournament")
                        .WithMany("Staff")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Stats", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.TMap", "Map")
                        .WithMany("Stats")
                        .HasForeignKey("MapId", "MapMod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Map");

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Team", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Tournament", null)
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Tournament", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Host", "Host")
                        .WithMany("Tournaments")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TournamentPlayer", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Player", "Player")
                        .WithMany("Tournaments")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.Tournament", "Tournament")
                        .WithMany("Players")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoundTMap", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", null)
                        .WithMany()
                        .HasForeignKey("RoundsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.TMap", null)
                        .WithMany()
                        .HasForeignKey("MappoolId", "MappoolMod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoundTMapSuggestion", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", null)
                        .WithMany()
                        .HasForeignKey("RoundsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.TMapSuggestion", null)
                        .WithMany()
                        .HasForeignKey("MapSuggestionsId", "MapSuggestionsMod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Host", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Player", b =>
                {
                    b.Navigation("Stats");

                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMap", b =>
                {
                    b.Navigation("Stats");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Tournament", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Rounds");

                    b.Navigation("Staff");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
