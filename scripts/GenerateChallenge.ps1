Write-Output "--- Advent of Code - Challenge generator ---`r`n`r`n"

$DayNumber = (Get-ChildItem -Path "..\src\AdventOfCode.Console\Challenges" -Directory |
              Select-Object -ExpandProperty Name |
              ForEach-Object { [int]($_ -replace '\D', '') } |
              Sort-Object |
              Select-Object -Last 1) + 1

$DayNumber = "{0:D2}" -f $DayNumber

$ChallengeDirectory = "src\AdventOfCode.Console\Challenges\Day$DayNumber"
$TestDirectory = "src\AdventOfCode.Tests"

if (Test-Path "..\$ChallengeDirectory") {
    Write-Warning "Challenge Day$DayNumber already exists."
    Read-Host -Prompt "Press Enter to exit"
    return
}

New-Item -Path "..\$ChallengeDirectory" -ItemType Directory | Out-Null
Write-Output "`r`nGenerated challenge directory: $ChallengeDirectory`r`n"

New-Item -Path "..\$ChallengeDirectory\Day$DayNumber.cs" -ItemType File | Out-Null
$ChallengeCode = (Get-Content -path "templates/Challenge.template") -replace "{{day_number}}", $DayNumber
Set-Content -Path "..\$ChallengeDirectory\Day$DayNumber.cs" -Value $ChallengeCode
Write-Output "Generated challenge code: Day$DayNumber.cs`r`n"

New-Item -Path "..\$ChallengeDirectory\Input.txt" -ItemType File | Out-Null
New-Item -Path "..\$ChallengeDirectory\Input_Part2.txt" -ItemType File | Out-Null
Write-Output "Generated challenge input files: Input.txt, Input_Part2.txt`r`n"

New-Item -Path "..\$ChallengeDirectory\Instruction.md" -ItemType File | Out-Null
Write-Output "Generated challenge instruction file: Instruction.md`r`n"

New-Item -Path "..\$TestDirectory\Day$DayNumberTests.cs" -ItemType File | Out-Null
$TestCode = (Get-Content -path "templates/Tests.template") -replace "{{day_number}}", $DayNumber
Set-Content -Path "..\$TestDirectory\Day$DayNumberTests.cs" -Value $TestCode
Write-Output "Generated test code: Day$DayNumberTests.cs`r`n"

Write-Output "--- Completed generation for day $DayNumber ---`r`n"
Read-Host -Prompt "Press Enter to exit"