-- Script para sincronizar a migration inicial com o banco existente
-- Execute este script se as tabelas já existem no banco

-- Inserir registro da migration na tabela de histórico
INSERT INTO dindyn.__EFMigrationsHistory (MigrationId, ProductVersion) 
VALUES ('20250909041440_InitialCreate', '8.0.0');

-- Verificar se as tabelas existem
SELECT 'Tabelas existentes:' as Status;
SHOW TABLES;

-- Verificar estrutura da tabela cliente
SELECT 'Estrutura da tabela cliente:' as Status;
DESCRIBE cliente;

-- Verificar estrutura da tabela token_acesso
SELECT 'Estrutura da tabela token_acesso:' as Status;
DESCRIBE token_acesso;
