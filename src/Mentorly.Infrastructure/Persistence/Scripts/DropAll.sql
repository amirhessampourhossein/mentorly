IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserTools_Users_UserCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserTools]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserTools] DROP CONSTRAINT [FK_UserTools_Users_UserCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserTools_Tools_ToolCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserTools]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserTools] DROP CONSTRAINT [FK_UserTools_Tools_ToolCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserSkills_Users_UserCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserSkills]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserSkills] DROP CONSTRAINT [FK_UserSkills_Users_UserCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserSkills_Skills_SkillCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserSkills]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserSkills] DROP CONSTRAINT [FK_UserSkills_Skills_SkillCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Users_Users_AffiliateCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Users]'))
ALTER TABLE [mersaair_ExpertConsultation].[Users] DROP CONSTRAINT [FK_Users_Users_AffiliateCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserExpertise_Users_UserCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserExpertise]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserExpertise] DROP CONSTRAINT [FK_UserExpertise_Users_UserCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserExpertise_Expertise_ExpertiseCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserExpertise]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserExpertise] DROP CONSTRAINT [FK_UserExpertise_Expertise_ExpertiseCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserDisciplines_Users_UserCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserDisciplines]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserDisciplines] DROP CONSTRAINT [FK_UserDisciplines_Users_UserCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_UserDisciplines_Disciplines_DisciplineCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserDisciplines]'))
ALTER TABLE [mersaair_ExpertConsultation].[UserDisciplines] DROP CONSTRAINT [FK_UserDisciplines_Disciplines_DisciplineCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Transactions_Sessions_SessionCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Transactions]'))
ALTER TABLE [mersaair_ExpertConsultation].[Transactions] DROP CONSTRAINT [FK_Transactions_Sessions_SessionCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Sessions_Users_InterviewerCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Sessions]'))
ALTER TABLE [mersaair_ExpertConsultation].[Sessions] DROP CONSTRAINT [FK_Sessions_Users_InterviewerCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Sessions_Users_IntervieweeCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Sessions]'))
ALTER TABLE [mersaair_ExpertConsultation].[Sessions] DROP CONSTRAINT [FK_Sessions_Users_IntervieweeCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Sessions_Bookings_BookingCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Sessions]'))
ALTER TABLE [mersaair_ExpertConsultation].[Sessions] DROP CONSTRAINT [FK_Sessions_Bookings_BookingCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Disciplines_Expertise_ExpertiseCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Disciplines]'))
ALTER TABLE [mersaair_ExpertConsultation].[Disciplines] DROP CONSTRAINT [FK_Disciplines_Expertise_ExpertiseCode]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[FK_Bookings_Users_UserCode]') AND parent_object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Bookings]'))
ALTER TABLE [mersaair_ExpertConsultation].[Bookings] DROP CONSTRAINT [FK_Bookings_Users_UserCode]

/****** Object:  Table [mersaair_ExpertConsultation].[UserTools]    Script Date: 15/02/1403 02:08:27 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserTools]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[UserTools]

/****** Object:  Table [mersaair_ExpertConsultation].[UserSkills]    Script Date: 15/02/1403 02:08:28 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserSkills]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[UserSkills]

/****** Object:  Table [mersaair_ExpertConsultation].[Users]    Script Date: 15/02/1403 02:08:28 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Users]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Users]

/****** Object:  Table [mersaair_ExpertConsultation].[UserExpertise]    Script Date: 15/02/1403 02:08:28 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserExpertise]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[UserExpertise]

/****** Object:  Table [mersaair_ExpertConsultation].[UserDisciplines]    Script Date: 15/02/1403 02:08:28 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[UserDisciplines]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[UserDisciplines]

/****** Object:  Table [mersaair_ExpertConsultation].[Transactions]    Script Date: 15/02/1403 02:08:29 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Transactions]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Transactions]

/****** Object:  Table [mersaair_ExpertConsultation].[Tools]    Script Date: 15/02/1403 02:08:29 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Tools]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Tools]

/****** Object:  Table [mersaair_ExpertConsultation].[Skills]    Script Date: 15/02/1403 02:08:29 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Skills]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Skills]

/****** Object:  Table [mersaair_ExpertConsultation].[Sessions]    Script Date: 15/02/1403 02:08:29 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Sessions]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Sessions]

/****** Object:  Table [mersaair_ExpertConsultation].[Expertise]    Script Date: 15/02/1403 02:08:30 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Expertise]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Expertise]

/****** Object:  Table [mersaair_ExpertConsultation].[Disciplines]    Script Date: 15/02/1403 02:08:30 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Disciplines]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Disciplines]

/****** Object:  Table [mersaair_ExpertConsultation].[Bookings]    Script Date: 15/02/1403 02:08:30 ق.ظ ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mersaair_ExpertConsultation].[Bookings]') AND type in (N'U'))
DROP TABLE [mersaair_ExpertConsultation].[Bookings]

