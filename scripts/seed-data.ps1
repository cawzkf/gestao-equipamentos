# Popula o banco com dados de exemplo (categorias, fornecedores e equipamentos).
#
# Pre-requisitos:
#   1. Banco no ar:  docker compose up -d
#   2. API no ar:    (rodando em http://localhost:5125)
#
# Como rodar (PowerShell, na pasta do projeto):
#   .\scripts\seed-data.ps1
#
# Se a sua API estiver em outra porta, passe a URL:
#   .\scripts\seed-data.ps1 -BaseUrl "http://localhost:5000"

param(
    [string]$BaseUrl = "http://localhost:5125",
    [string]$Email   = "seed@exemplo.com",
    [string]$Password = "Senha123!"
)

$ErrorActionPreference = "Stop"

function Send-Json($Method, $Url, $Body, $Headers) {
    $json = if ($Body) { $Body | ConvertTo-Json } else { $null }
    return Invoke-RestMethod -Uri $Url -Method $Method -Headers $Headers -Body $json -ContentType "application/json"
}

Write-Host "== Populando $BaseUrl ==" -ForegroundColor Cyan

# 1) Usuario: tenta registrar; se ja existir (409), faz login.
try {
    $auth = Send-Json POST "$BaseUrl/api/auth/register" @{ name = "Seed"; email = $Email; password = $Password } @{}
    Write-Host "Usuario criado: $Email"
} catch {
    $auth = Send-Json POST "$BaseUrl/api/auth/login" @{ email = $Email; password = $Password } @{}
    Write-Host "Usuario ja existia, login feito: $Email"
}
$H = @{ Authorization = "Bearer $($auth.token)" }

# 2) Categorias
$cats = @{}
foreach ($n in "Notebooks", "Monitores", "Impressoras", "Roteadores") {
    $r = Send-Json POST "$BaseUrl/api/category" @{ name = $n } $H
    $cats[$n] = $r.id
    Write-Host "Categoria: $n (id $($r.id))"
}

# 3) Fornecedores
$sups = @{}
$supList = @(
    @{ Name = "Dell Brasil"; ContactEmail = "vendas@dell.com" },
    @{ Name = "HP Inc";      ContactEmail = "contato@hp.com" },
    @{ Name = "TP-Link";     ContactEmail = "suporte@tplink.com" }
)
foreach ($s in $supList) {
    $r = Send-Json POST "$BaseUrl/api/supplier" $s $H
    $sups[$s.Name] = $r.id
    Write-Host "Fornecedor: $($s.Name) (id $($r.id))"
}

# 4) Equipamentos (referenciam os ids das categorias e fornecedores acima)
$eqs = @(
    @{ Name = "Notebook Latitude 5440"; SerialNumber = "DL-NB-001"; Model = "Latitude 5440";     PurchaseDate = "2025-01-15"; CategoryId = $cats["Notebooks"];   SupplierId = $sups["Dell Brasil"] },
    @{ Name = "Notebook Inspiron 15";   SerialNumber = "DL-NB-002"; Model = "Inspiron 15 3520";   PurchaseDate = "2025-03-10"; CategoryId = $cats["Notebooks"];   SupplierId = $sups["Dell Brasil"] },
    @{ Name = "Monitor HP E24";         SerialNumber = "HP-MON-001"; Model = "E24 G5";            PurchaseDate = "2025-02-20"; CategoryId = $cats["Monitores"];   SupplierId = $sups["HP Inc"] },
    @{ Name = "Impressora LaserJet";    SerialNumber = "HP-IMP-001"; Model = "LaserJet Pro M404"; PurchaseDate = "2024-11-05"; CategoryId = $cats["Impressoras"]; SupplierId = $sups["HP Inc"] },
    @{ Name = "Roteador Archer";        SerialNumber = "TP-RT-001"; Model = "Archer AX55";        PurchaseDate = "2025-04-01"; CategoryId = $cats["Roteadores"];  SupplierId = $sups["TP-Link"] }
)
foreach ($e in $eqs) {
    $r = Send-Json POST "$BaseUrl/api/equipment" $e $H
    Write-Host "Equipamento: $($e.Name) (id $($r.id))"
}

Write-Host "== Pronto! Banco populado. ==" -ForegroundColor Green
