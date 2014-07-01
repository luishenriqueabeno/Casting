create table TipoDePeles (
    Id INT IDENTITY NOT NULL,
   Nome NVARCHAR(255) null,
   primary key (Id)
)

alter table Avaliacaos 
	add TipoDePele_Id INT null

alter table Avaliacaos 
    add constraint FKB1521907BFE71B74 
    foreign key (TipoDePele_Id) 
    references TipoDePeles
    
    
        create table PerfilComportalmentalToAvaliacao (
    Avaliacao_Id INT not null,
   PerfilComportalmental_Id INT not null
)

create table AvaliacaosToPerfilProfissionals (
    Avaliacao_Id INT not null,
   PerfilProfissional_Id INT not null,
   PerfilComportalmental_Id INT not null
)

create table AvaliacaosToConhecimentoMerchandisings (
    Avaliacao_Id INT not null,
   ConhecimentoMerchandising_Id INT not null
)


alter table Candidatos 
	add SituacaoCandidato_Id INT null

alter table Avaliacaos 
	add SituacaoCandidato_Id INT null

alter table Avaliacaos 
    add constraint FKB15219078D8C27AB 
    foreign key (SituacaoCandidato_Id) 
    references SituacaoCandidatos
    
alter table Candidatos 
    add constraint FK80B793C58D8C27AB 
    foreign key (SituacaoCandidato_Id) 
    references SituacaoCandidatos

