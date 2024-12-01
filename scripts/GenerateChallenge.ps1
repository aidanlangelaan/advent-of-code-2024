function Get-NextDayNumber {
    $lastDayNumber = (Get-ChildItem -Path "..\src\AdventOfCode.Console\Challenges" -Directory |
            Select-Object -ExpandProperty Name |
            ForEach-Object { [int]($_ -replace '\D', '') } |
            Sort-Object |
            Select-Object -Last 1)
    return "{0:D2}" -f ($lastDayNumber + 1)
}

function New-File {
    param (
        [string]$Path,
        [string]$Content = ""
    )
    New-Item -Path $Path -ItemType File -Force | Out-Null
    if ($Content) {
        $formattedContent = $Content -replace "`r?`n", "`r`n"
        Set-Content -Path $Path -Value $formattedContent -Encoding UTF8
    }
}

function Add-EmbeddedResource {
    param (
        [string]$ProjectPath,
        [string]$RelativePath
    )
    $projectFileContent = Get-Content $ProjectPath
    if (-not ($projectFileContent -match [regex]::Escape($RelativePath))) {
        $newItem = "    <ItemGroup>`r`n        <EmbeddedResource Include=`"$RelativePath`">`r`n            <CopyToOutputDirectory>Always</CopyToOutputDirectory>`r`n        </EmbeddedResource>`r`n    </ItemGroup>"
        $updatedContent = $projectFileContent -replace "(<\/Project>)", "$newItem`r`n</Project>"
        Set-Content -Path $ProjectPath -Value $updatedContent
    }
}

Write-Output "--- Advent of Code - Challenge generator ---`r`n"

$DayNumber = Get-NextDayNumber
$ChallengeDirectory = "..\src\AdventOfCode.Console\Challenges\Day$DayNumber"
$TestDirectory = "..\src\AdventOfCode.Tests"

if (Test-Path $ChallengeDirectory) {
    Write-Warning "Challenge Day$DayNumber already exists."
    Read-Host -Prompt "Press Enter to exit"
    return
}

New-Item -Path $ChallengeDirectory -ItemType Directory | Out-Null
Write-Output "Generated directory: $ChallengeDirectory`r`n"

$challengeCode = (Get-Content -Path "templates/Challenge.template" -Raw) -replace "{{day_number}}", $DayNumber
New-File -Path "$ChallengeDirectory\Day$DayNumber.cs" -Content $challengeCode
Write-Output "Generated code file: Day$DayNumber.cs`r`n"

New-File -Path "$ChallengeDirectory\Input1.txt"
New-File -Path "$ChallengeDirectory\Input2.txt"
Write-Output "Generated input files: Input1.txt, Input2.txt`r`n"

New-File -Path "$ChallengeDirectory\.solution"
Write-Output "Generated solution file: .solution`r`n"

New-File -Path "$ChallengeDirectory\Instruction.md"
Write-Output "Generated instruction file: Instruction.md`r`n"

$testCode = (Get-Content -Path "templates/Tests.template" -Raw) -replace "{{day_number}}", $DayNumber
New-File -Path "$TestDirectory\Day${DayNumber}Tests.cs" -Content $testCode
Write-Output "Generated test file: Day${DayNumber}Tests.cs`r`n"

$ProjectPath = "..\src\AdventOfCode.Console\AdventOfCode.Console.csproj"
Add-EmbeddedResource -ProjectPath $ProjectPath -RelativePath "Challenges\Day$DayNumber\Input1.txt"
Add-EmbeddedResource -ProjectPath $ProjectPath -RelativePath "Challenges\Day$DayNumber\Input2.txt"

Write-Output "Added new Day$DayNumber to the project file`r`n"
Write-Output "--- Completed generation for day $DayNumber ---`r`n"
Read-Host -Prompt "Press Enter to exit"
