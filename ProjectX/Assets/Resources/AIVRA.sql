-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 15, 2020 at 11:40 PM
-- Server version: 10.4.14-MariaDB
-- PHP Version: 7.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: 'media_form'
--

-- --------------------------------------------------------

--
-- Table structure for table 'failed_jobs'
--

/*CREATE TABLE 'failed_jobs' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'uuid' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'connection' text COLLATE utf8mb4_unicode_ci NOT NULL,
  'queue' text COLLATE utf8mb4_unicode_ci NOT NULL,
  'payload' longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  'exception' longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  'failed_at' timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

-- --------------------------------------------------------

--
-- Table structure for table 'migrations'
--

/*CREATE TABLE 'migrations' (
  'id' int(10) UNSIGNED NOT NULL,
  'migration' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'batch' int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

--
-- Dumping data for table 'migrations'
--

/*INSERT INTO 'migrations' ('id', 'migration', 'batch') VALUES
(1, '2014_10_12_000000_create_users_table', 1),
(2, '2014_10_12_100000_create_password_resets_table', 1),
(3, '2019_08_19_000000_create_failed_jobs_table', 1),
(4, '2014_10_12_200000_add_two_factor_columns_to_users_table', 2),
(5, '2019_12_14_000001_create_personal_access_tokens_table', 2),
(6, '2020_11_02_065851_create_sessions_table', 2),
(7, '2020_11_02_070056_create_tasks_table', 2);*/

-- --------------------------------------------------------

--
-- Table structure for table 'password_resets'
--

/*CREATE TABLE 'password_resets' (
  'email' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'token' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'created_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

--
-- Dumping data for table 'password_resets'
--

/*INSERT INTO 'password_resets' ('email', 'token', 'created_at') VALUES
('gmurray@neonsky.net', '$2y$10$RzbHNJ5OENK0GWaFnkqrf.mfu1C3YhLrI2YdOiJSn9iO9p9/O1/M2', '2020-11-03 04:44:25');*/

-- --------------------------------------------------------

--
-- Table structure for table 'personal_access_tokens'
--

/*CREATE TABLE 'personal_access_tokens' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'tokenable_type' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'tokenable_id' bigint(20) UNSIGNED NOT NULL,
  'name' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'token' varchar(64) COLLATE utf8mb4_unicode_ci NOT NULL,
  'abilities' text COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'last_used_at' timestamp NULL DEFAULT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

-- --------------------------------------------------------

--
-- Table structure for table 'sessions'
--

/*CREATE TABLE 'sessions' (
  'id' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'user_id' bigint(20) UNSIGNED DEFAULT NULL,
  'ip_address' varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'user_agent' text COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'payload' text COLLATE utf8mb4_unicode_ci NOT NULL,
  'last_activity' int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

--
-- Dumping data for table 'sessions'
--

/*INSERT INTO 'sessions' ('id', 'user_id', 'ip_address', 'user_agent', 'payload', 'last_activity') VALUES
('1I1VivusQCzAMEQTOSXTokPUi20beef9ThF5ZR5p', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoicmVEaWl2cUt4QW5DSzg0RVFRUHQycEViZ2Y0TEZhYVl2VXhLTkVDdSI7czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MjE6Imh0dHA6Ly8xMjcuMC4wLjE6ODAwMCI7fX0=', 1604379627),
('iX5LvIDUxcivFEpPNB0SUhCDwxEZz7uAGyzsV4xq', 1, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', 'YTo2OntzOjY6Il90b2tlbiI7czo0MDoiWG93TldSblY2enpaM0lVTzdOQWw1b1VoMEEyekVqZDI5dENwTmQybiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MzE6Imh0dHA6Ly8xMjcuMC4wLjE6ODAwMC9kYXNoYm9hcmQiO31zOjY6Il9mbGFzaCI7YToyOntzOjM6Im9sZCI7YTowOnt9czozOiJuZXciO2E6MDp7fX1zOjUwOiJsb2dpbl93ZWJfNTliYTM2YWRkYzJiMmY5NDAxNTgwZjAxNGM3ZjU4ZWE0ZTMwOTg5ZCI7aToxO3M6MTc6InBhc3N3b3JkX2hhc2hfd2ViIjtzOjYwOiIkMnkkMTAkSnUuWmhxb3lLdmc1d0hFRE5yZHRqdUlPTmY2alNneC5MT1ZweGtnS1doUWtxNnhEY0g3ZW0iO3M6MjE6InBhc3N3b3JkX2hhc2hfc2FuY3R1bSI7czo2MDoiJDJ5JDEwJEp1LlpocW95S3ZnNXdIRUROcmR0anVJT05mNmpTZ3guTE9WcHhrZ0tXaFFrcTZ4RGNIN2VtIjt9', 1605130386),
('OH5knh34zLaGVgE2vQAH7gyxhKdV2AZWXn42QFx3', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiRnZoelZHN1RFbDhYZ1hvSjhEclVqN2hEV044bHZQNkxMRFpCTUt5UCI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MjE6Imh0dHA6Ly8xMjcuMC4wLjE6ODAwMCI7fXM6NjoiX2ZsYXNoIjthOjI6e3M6Mzoib2xkIjthOjA6e31zOjM6Im5ldyI7YTowOnt9fX0=', 1604468744),
('Ujg6GUv7bDTJZMOCJOt0ltef9epNu6ofuXQoMa64', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiUWtkaExNSzcwSU5velI4dzNPc3M2bVNpaUVQQ1N0amNtNktlRU9xbSI7czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MjE6Imh0dHA6Ly8xMjcuMC4wLjE6ODAwMCI7fX0=', 1604357887),
('ZsO7LeK4vfQAy2hyE5TgIgOmCEizvgVHrPxNxLtF', 1, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36', 'YTo2OntzOjY6Il90b2tlbiI7czo0MDoiRWZ0VWgzVXRaeHpFRnRMaVNJMzBleDB2WmZlWmdNRW83WThNWEg2cCI7czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MzE6Imh0dHA6Ly8xMjcuMC4wLjE6ODAwMC9kYXNoYm9hcmQiO31zOjUwOiJsb2dpbl93ZWJfNTliYTM2YWRkYzJiMmY5NDAxNTgwZjAxNGM3ZjU4ZWE0ZTMwOTg5ZCI7aToxO3M6MTc6InBhc3N3b3JkX2hhc2hfd2ViIjtzOjYwOiIkMnkkMTAkSnUuWmhxb3lLdmc1d0hFRE5yZHRqdUlPTmY2alNneC5MT1ZweGtnS1doUWtxNnhEY0g3ZW0iO3M6MjE6InBhc3N3b3JkX2hhc2hfc2FuY3R1bSI7czo2MDoiJDJ5JDEwJEp1LlpocW95S3ZnNXdIRUROcmR0anVJT05mNmpTZ3guTE9WcHhrZ0tXaFFrcTZ4RGNIN2VtIjt9', 1604306515);*/

-- --------------------------------------------------------

--
-- Table structure for table 'tasks'
--

/*CREATE TABLE 'tasks' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'description' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'user_id' int(10) UNSIGNED NOT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;*/

--
-- Dumping data for table 'tasks'
--

/*INSERT INTO 'tasks' ('id', 'description', 'user_id', 'created_at', 'updated_at') VALUES
(2, 'This is task 2', 1, '2020-11-03 04:31:11', '2020-11-03 04:31:11');*/

-- --------------------------------------------------------

-- Fields (UserAccounts): ID (PK), fullName, username, password, email, phoneNumber, birthdate
-- Table structure for table 'UserAccounts'
--

CREATE TABLE 'UserAccounts' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'fullName' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'username' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'email' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'phoneNumber' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'birthdate' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'email_verified_at' timestamp NULL DEFAULT NULL,
  'password' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'two_factor_secret' text COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'two_factor_recovery_codes' text COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'remember_token' varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table 'UserAccounts'
--

INSERT INTO 'UserAccounts' ('id', 'fullName', 'username', 'email', 'phoneNumber', 'birthdate', 'email_verified_at', 'password', 'two_factor_secret', 'two_factor_recovery_codes', 'remember_token', 'created_at', 'updated_at') VALUES
(1, 'GlenMurray', 'gmurray', 'gmurray@neonsky.net', '2396729507', '05/28/1984', NULL, '$2y$10$Ju.ZhqoyKvg5wHEDNrdtjuIONf6jSgx.LOVpxkgKWhQkq6xDcH7em', NULL, NULL, NULL, '2020-11-02 13:48:13', '2020-11-02 13:48:13');

-- Fields (Businesses): ID (FK), businessName (PK), businessAddress
-- Table structure for table 'UserAccounts'
--

CREATE TABLE 'Businesses' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'businessName' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'businessAddress' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table 'Businesses'
--

INSERT INTO 'Businesses' ('id', 'businessName', 'businessAddress', 'created_at', 'updated_at') VALUES
(1, 'Neonsky', '10 N. Grove St Chippewa Falls WI 54729', '2020-11-02 13:48:13', '2020-11-02 13:48:13');

-- Fields (BusinessDeals): ID (PK), businessName (FK), percentage, flatAmount, startDate, endDate
-- Table structure for table 'BusinessDeals'
--

CREATE TABLE 'BusinessDeals' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'businessName' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'percentage' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'flatAmount' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'startDate' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'endDate' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table 'BusinessDeals'
--

INSERT INTO 'BusinessDeals' ('id', 'businessName', 'percentage', 'flatAmount', 'startDate', 'endDate', 'created_at', 'updated_at') VALUES
(1, 'Neonsky', '22', '35', '2020-11-15 13:48:13', '2020-12-15 13:48:13', '2020-11-02 13:48:13', '2020-11-02 13:48:13');

-- Fields (CustomerDeals): ID (FK), username
-- Table structure for table 'CustomerDeals'
--

CREATE TABLE 'CustomerDeals' (
  'id' bigint(20) UNSIGNED NOT NULL,
  'username' varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  'created_at' timestamp NULL DEFAULT NULL,
  'updated_at' timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table 'CustomerDeals'
--

INSERT INTO 'CustomerDeals' ('id', 'username', 'created_at', 'updated_at') VALUES
(1, 'gmurray', '2020-11-02 13:48:13', '2020-11-02 13:48:13');

--
-- Indexes for dumped tables
--

--
-- Indexes for table 'failed_jobs'
--
/*ALTER TABLE 'failed_jobs'
  ADD PRIMARY KEY ('id'),
  ADD UNIQUE KEY 'failed_jobs_uuid_unique' ('uuid');

--
-- Indexes for table 'migrations'
--
ALTER TABLE 'migrations'
  ADD PRIMARY KEY ('id');

--
-- Indexes for table 'password_resets'
--
ALTER TABLE 'password_resets'
  ADD KEY 'password_resets_email_index' ('email');

--
-- Indexes for table 'personal_access_tokens'
--
ALTER TABLE 'personal_access_tokens'
  ADD PRIMARY KEY ('id'),
  ADD UNIQUE KEY 'personal_access_tokens_token_unique' ('token'),
  ADD KEY 'personal_access_tokens_tokenable_type_tokenable_id_index' ('tokenable_type','tokenable_id');

--
-- Indexes for table 'sessions'
--
ALTER TABLE 'sessions'
  ADD PRIMARY KEY ('id'),
  ADD KEY 'sessions_user_id_index' ('user_id'),
  ADD KEY 'sessions_last_activity_index' ('last_activity');

--
-- Indexes for table 'tasks'
--
ALTER TABLE 'tasks'
  ADD PRIMARY KEY ('id'),
  ADD KEY 'tasks_user_id_index' ('user_id');

--
-- Indexes for table 'users'
--
ALTER TABLE 'users'
  ADD PRIMARY KEY ('id'),
  ADD UNIQUE KEY 'users_email_unique' ('email');

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table 'failed_jobs'
--
ALTER TABLE 'failed_jobs'
  MODIFY 'id' bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table 'migrations'
--
ALTER TABLE 'migrations'
  MODIFY 'id' int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table 'personal_access_tokens'
--
ALTER TABLE 'personal_access_tokens'
  MODIFY 'id' bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table 'tasks'
--
ALTER TABLE 'tasks'
  MODIFY 'id' bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table 'users'
--
ALTER TABLE 'users'
  MODIFY 'id' bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;*/

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
