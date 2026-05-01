@echo off
echo ==========================================
echo Criando estrutura de pastas do projeto...
echo ==========================================

REM ===== ART =====
mkdir Art
mkdir Art\Characters
mkdir Art\Environment
mkdir Art\Props
mkdir Art\Textures

REM ===== CORE ASSETS =====
mkdir Materials
mkdir Shaders
mkdir VFX
mkdir Prefabs

REM ===== SCENES =====
mkdir Scenes
mkdir Scenes\Main
mkdir Scenes\Test

REM ===== OUTROS =====
mkdir Scripts
mkdir Audio
mkdir UI

echo.
echo Estrutura criada com sucesso!
pause