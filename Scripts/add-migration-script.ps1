function Add-Migration {
    # [CmdletBinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(
            Mandatory=$true,
            ValueFromPipeline=$true,
            ValueFromPipelineByPropertyName=$true
        )]
        [String[]]
        $Path ,
    
        [String]
        $Name
    )
    
    Process {
        foreach($p in $Path) {
            if(Test-Path $p) {
                Set-Location $p
                dotnet ef --startup-project D:\LabWebApp\Web migrations add $Name
                Write-Host "Add migration script executed successfully" -BackgroundColor Green -ForegroundColor Black
                Set-Location D:\LabWebApp\Scripts
            }
            else{
                Write-Host "Add migration script failed (wrong path)" -BackgroundColor Red -ForegroundColor Black
            }
        }
    }
}

Write-Host "Migrations list:" -BackgroundColor Yellow -ForegroundColor Black
dotnet ef migrations list --project D:\LabWebApp\Web
$Name = Read-Host "Enter name of new migration and press Enter"
Add-Migration D:\LabWebApp\DAL $Name 
