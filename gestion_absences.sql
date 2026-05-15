-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : ven. 15 mai 2026 à 08:23
-- Version du serveur : 8.4.7
-- Version de PHP : 8.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gestion_absences`
--
CREATE DATABASE IF NOT EXISTS `gestion_absences` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `gestion_absences`;

CREATE USER IF NOT EXISTS 'A2'@'localhost' IDENTIFIED BY 'mdp!';
GRANT ALL PRIVILEGES ON gestion_absences.* TO 'A2'@'localhost';
FLUSH PRIVILEGES;

-- --------------------------------------------------------

--
-- Structure de la table `absence`
--

DROP TABLE IF EXISTS `absence`;
CREATE TABLE IF NOT EXISTS `absence` (
  `idpersonnel` int NOT NULL,
  `datedebut` datetime NOT NULL,
  `datefin` datetime DEFAULT NULL,
  `idmotif` int NOT NULL,
  PRIMARY KEY (`idpersonnel`,`datedebut`),
  KEY `idmotif` (`idmotif`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `absence`
--

INSERT INTO `absence` (`idpersonnel`, `datedebut`, `datefin`, `idmotif`) VALUES
(8, '2026-03-20 00:00:00', '2026-04-11 19:06:28', 2),
(1, '2025-09-11 00:00:00', '2026-02-09 19:06:36', 4),
(6, '2025-07-15 00:00:00', '2026-05-10 19:06:48', 2),
(1, '2026-03-29 00:00:00', '2026-05-17 19:07:06', 3),
(1, '2025-06-23 00:00:00', '2025-08-11 19:08:38', 1),
(10, '2025-06-07 00:00:00', '2025-07-01 19:08:32', 3),
(1, '2025-08-06 00:00:00', '2025-09-10 19:08:17', 1),
(8, '2026-01-14 00:00:00', '2026-02-24 19:07:09', 3),
(2, '2025-09-17 00:00:00', '2025-10-23 19:08:09', 2),
(2, '2025-12-12 00:00:00', '2026-01-05 19:08:13', 1),
(1, '2025-11-30 00:00:00', '2026-05-10 19:07:13', 2),
(2, '2025-12-27 00:00:00', '2026-01-13 19:07:59', 4),
(10, '2025-08-25 00:00:00', '2025-09-09 19:08:04', 3),
(4, '2025-12-23 00:00:00', '2026-01-03 19:07:15', 4),
(4, '2025-07-25 00:00:00', '2025-12-16 19:07:50', 2),
(5, '2025-07-11 00:00:00', '2025-12-15 19:07:54', 2),
(9, '2025-10-18 00:00:00', '2026-01-06 19:07:44', 4),
(7, '2025-10-14 00:00:00', '2026-04-03 19:07:17', 2),
(4, '2026-01-23 00:00:00', '2026-03-10 19:07:36', 1),
(6, '2025-07-20 00:00:00', '2026-03-10 19:07:40', 3),
(1, '2025-11-21 00:00:00', '2026-03-10 19:07:33', 2),
(10, '2026-04-27 00:00:00', '2026-05-11 19:07:20', 4),
(7, '2026-03-24 00:00:00', '2026-03-10 19:07:26', 2),
(9, '2025-08-23 00:00:00', '2026-02-09 19:07:29', 2),
(9, '2026-02-21 00:00:00', '2026-05-08 19:07:24', 2),
(3, '2025-06-08 00:00:00', '2025-07-08 19:08:48', 4),
(10, '2025-08-02 00:00:00', '2025-09-09 19:08:55', 1),
(10, '2026-02-15 00:00:00', '2026-03-04 19:09:01', 2),
(5, '2026-05-04 00:00:00', '2026-05-15 19:09:09', 1),
(2, '2025-05-25 00:00:00', '2025-06-09 19:09:19', 4),
(2, '2026-05-05 00:00:00', '2026-05-06 19:09:43', 3),
(5, '2026-04-30 00:00:00', '2026-04-07 19:09:48', 2),
(7, '2026-01-07 00:00:00', '2026-01-07 19:09:52', 3),
(6, '2025-09-17 00:00:00', '2025-10-08 19:09:59', 1),
(2, '2025-10-18 00:00:00', '2025-11-12 19:10:04', 1),
(3, '2026-02-24 00:00:00', '2026-02-28 19:10:13', 2),
(6, '2025-10-31 00:00:00', '2025-11-19 19:11:51', 4),
(7, '2025-11-17 00:00:00', '2025-12-07 19:12:02', 3),
(2, '2026-02-03 00:00:00', '2026-03-01 19:12:16', 4),
(3, '2025-11-05 00:00:00', '2025-11-12 00:00:00', 2),
(8, '2025-10-09 00:00:00', '2025-11-04 00:00:00', 2),
(10, '2026-05-09 00:00:00', '2026-04-02 00:00:00', 1),
(2, '2025-06-27 00:00:00', '2025-07-03 19:14:30', 2),
(5, '2026-02-24 00:00:00', '2026-03-03 19:14:47', 3),
(1, '2025-10-17 00:00:00', '2025-11-11 00:00:00', 2),
(8, '2026-01-15 00:00:00', '2026-02-02 19:15:21', 2),
(6, '2025-09-28 00:00:00', '2025-10-02 19:13:15', 1),
(10, '2026-02-02 00:00:00', '2026-04-06 19:11:14', 3),
(7, '2025-05-15 00:00:00', '2025-06-02 19:11:06', 2),
(10, '2025-07-09 00:00:00', '2026-01-01 19:10:45', 3),
(1, '2025-03-17 19:03:00', '2025-05-13 19:03:00', 4);

-- --------------------------------------------------------

--
-- Structure de la table `motif`
--

DROP TABLE IF EXISTS `motif`;
CREATE TABLE IF NOT EXISTS `motif` (
  `idmotif` int NOT NULL AUTO_INCREMENT,
  `libelle` varchar(128) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`idmotif`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `motif`
--

INSERT INTO `motif` (`idmotif`, `libelle`) VALUES
(1, 'vacances'),
(2, 'maladie'),
(3, 'motif familial'),
(4, 'congé parental');

-- --------------------------------------------------------

--
-- Structure de la table `personnel`
--

DROP TABLE IF EXISTS `personnel`;
CREATE TABLE IF NOT EXISTS `personnel` (
  `idpersonnel` int NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `prenom` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `tel` varchar(15) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `mail` varchar(128) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `idservice` int NOT NULL,
  PRIMARY KEY (`idpersonnel`),
  KEY `idservice` (`idservice`)
) ENGINE=MyISAM AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `personnel`
--

INSERT INTO `personnel` (`idpersonnel`, `nom`, `prenom`, `tel`, `mail`, `idservice`) VALUES
(1, 'Callingham', 'Elfrieda', '9295475076', 'ecallingham0@woothemes.com', 3),
(2, 'Dumbrell', 'Marlane', '4524657310', 'mdumbrell1@diigo.com', 1),
(3, 'Ridoutt', 'Tad', '9166024100', 'tridoutt2@mac.com', 2),
(4, 'Goudge', 'Deloris', '8224574981', 'dgoudge3@weibo.com', 1),
(5, 'Geany', 'Angelica', '8347969203', 'ageany4@1und1.de', 2),
(6, 'Unsworth', 'Salome', '6563214819', 'sunsworth5@state.gov', 2),
(7, 'Machans', 'Lemmie', '4893264786', 'lmachans6@twitpic.com', 2),
(8, 'Skoate', 'Mitchell', '9789754910', 'mskoate7@umn.edu', 1),
(9, 'Hansel', 'Ermentrude', '2401244842', 'ehansel8@tmall.com', 3),
(10, 'Curbishley', 'Arel', '6744499381', 'acurbishley9@omniture.com', 2);

-- --------------------------------------------------------

--
-- Structure de la table `responsable`
--

DROP TABLE IF EXISTS `responsable`;
CREATE TABLE IF NOT EXISTS `responsable` (
  `login` varchar(64) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `pwd` varchar(64) COLLATE utf8mb4_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `responsable`
--

INSERT INTO `responsable` (`login`, `pwd`) VALUES
('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918');

-- --------------------------------------------------------

--
-- Structure de la table `service`
--

DROP TABLE IF EXISTS `service`;
CREATE TABLE IF NOT EXISTS `service` (
  `idservice` int NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`idservice`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Déchargement des données de la table `service`
--

INSERT INTO `service` (`idservice`, `nom`) VALUES
(1, 'administratif'),
(2, 'médiation culturelle'),
(3, 'prêt');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
