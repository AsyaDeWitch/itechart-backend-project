function Add-Migration {
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [String]
        $Path ,
    
        [String]
        $Name
    )
    
    Process {
            if(Test-Path ($Path + "\DAL")) {
                $loc = Get-Location
                Set-Location ($Path + "\DAL")
                dotnet ef --startup-project ($Path + "\Web") migrations add $Name
                Set-Location $loc
            }

            if($Name -ne "") {
                Write-Host "Add migration script executed successfully" -BackgroundColor Green -ForegroundColor Black
            }
            else{
                Write-Host "Add migration script failed (empty migration name)" -BackgroundColor Red -ForegroundColor Black
            }
    }
}

$p = Read-Host "Enter application folder path and press Enter"
Write-Host "Migrations list:" -BackgroundColor Yellow -ForegroundColor Black
if($p -ne "") {
    if(Test-Path ($p + "\Web")) {
        dotnet ef migrations list --project ($p + "\Web")
        $n = Read-Host "Enter name of new migration and press Enter"
        Add-Migration $p $n
    }
    else {
        Write-Host "Add migration script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
    } 
}
else {
    if(Test-Path D:\LabWebApp\Web) {
        dotnet ef migrations list --project D:\LabWebApp\Web
        $n = Read-Host "Enter name of new migration and press Enter"
        Add-Migration D:\LabWebApp $n
    }
}

