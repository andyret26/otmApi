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
    [Migration("20240423115320_Round")]
    partial class Round
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

            modelBuilder.Entity("OtmApi.Data.Entities.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<List<string>>("Roles")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

                    b.Property<List<string>>("Mods")
                        .HasColumnType("text[]");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("MapId", "PlayerId", "RoundId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ar")
                        .HasColumnType("numeric");

                    b.Property<int>("Bpm")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cs")
                        .HasColumnType("numeric");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Mapper")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<decimal>("Od")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sr")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("TMap");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMapSuggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ar")
                        .HasColumnType("numeric");

                    b.Property<int>("Bpm")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cs")
                        .HasColumnType("numeric");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Mapper")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<decimal>("Od")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sr")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("TMapSuggestion");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("PlayerTournament", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("integer");

                    b.HasKey("PlayersId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("TournamentPlayer", (string)null);
                });

            modelBuilder.Entity("StaffTournament", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("integer");

                    b.HasKey("StaffId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("TournamentStaff", (string)null);
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Round", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Tournament", null)
                        .WithMany("Rounds")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Stats", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.TMap", "Map")
                        .WithMany("Stats")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("Map");

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMap", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", "Round")
                        .WithMany("Mappool")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Round");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMapSuggestion", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Round", "Round")
                        .WithMany("MapSuggestions")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("PlayerTournament", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StaffTournament", b =>
                {
                    b.HasOne("OtmApi.Data.Entities.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OtmApi.Data.Entities.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
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
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Round", b =>
                {
                    b.Navigation("MapSuggestions");

                    b.Navigation("Mappool");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.TMap", b =>
                {
                    b.Navigation("Stats");
                });

            modelBuilder.Entity("OtmApi.Data.Entities.Tournament", b =>
                {
                    b.Navigation("Rounds");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
