function Generate-SQL {
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [String]
        $Path 
    )
    
    Process {
        if(Test-Path ($Path + "\DAL")) {
            $loc = Get-Location
            Set-Location ($Path + "\DAL")
            dotnet ef --startup-project ($Path + "\Web") migrations script | Out-File -FilePath ($Path + "\SQLScripts\" + (Get-Date -format "yyyyMMddHHmmss") + "_sqlScript.sql")
            Write-Host "Generate SQL script executed successfully" -BackgroundColor Green -ForegroundColor Black
            Set-Location $loc
        }
        else {
            Write-Host "Generate SQL script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
        }
    }
}

$p = Read-Host "Enter application folder path and press Enter"
if($p -ne "") {
    if(Test-Path ($p)) {
        Generate-SQL $p
    }
    else {
        Write-Host "Generate SQL script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
    } 
}
else {
    if(Test-Path D:\LabWebApp) {
        Generate-SQL D:\LabWebApp
    }
}