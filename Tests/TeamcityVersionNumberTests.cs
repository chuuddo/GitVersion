using GitFlowVersion;
using NUnit.Framework;

[TestFixture]
public class TeamCityVersionNumberTests
{

    [Test]
    public void Develop_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Unstable
                                             }
                               };
        VerifyOutput("0.0.0-Unstable4",versionAndBranch);
    }
    [Test]
    public void Develop_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 6,
                                                 Stability = Stability.Unstable
                                             }
                               };
        VerifyOutput("0.0.0-Unstable4.6",versionAndBranch);
    }


    [Test]
    public void Release_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Release,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Beta,
                                             }
                               };
        VerifyOutput("0.0.0-Beta4",versionAndBranch);
    }
    [Test]
    public void Release_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Release,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 8,
                                                 Stability = Stability.Beta,
                                             }
                               };
        VerifyOutput("0.0.0-Beta4.8",versionAndBranch);
    }

    [Test]
    public void Hotfix_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Hotfix,
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Beta,
                                                 PreReleasePartOne = 4
                                             }
                               };
        VerifyOutput("0.0.0-Beta4", versionAndBranch);
    }
    [Test]
    public void Hotfix_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Hotfix,
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Beta,
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 7,
                                             }
                               };
        VerifyOutput("0.0.0-Beta4.7", versionAndBranch);
    }


    [Test]
    public void Pull_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.PullRequest,
                                   Version = new SemanticVersion
                                             {
                                                 Suffix = "1571",
                                                 PreReleasePartOne = 131231232, //ignored
                                                 PreReleasePartTwo = 131231232, //ignored
                                                 Stability = Stability.Unstable
                                             }

                               };
        VerifyOutput("0.0.0-PullRequest-1571", versionAndBranch);
    }


    [Test]
    public void Feature_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Feature,
                                   Sha = "TheSha",
                                   BranchName = "AFeature",
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4, //ignored
                                                 PreReleasePartTwo = 4, //ignored
                                                 Stability = Stability.Unstable
                                             }
                               };
        VerifyOutput("0.0.0-Feature-AFeature-TheSha", versionAndBranch);
    }


    [Test]
    public void Master_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Final,
                                                 Suffix = "1571", //ignored
                                                 PreReleasePartOne = 131231232, //ignored
                                                 PreReleasePartTwo = 131231232 //ignored
                                             }
                               };
        VerifyOutput("0.0.0", versionAndBranch);
    }


    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_one_digit()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0004"));

    }
    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_one_digit_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 5,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0004.0005"));

    }
    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_two_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 40,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0040"));
    }
    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_two_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 40,
                                                 PreReleasePartTwo = 50,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0040.0050"));
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_three_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 400,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0400"));
    }
    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_three_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 400,
                                                 PreReleasePartTwo = 500,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable0400.0500"));
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_four_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4000,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable4000"));
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_four_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4000,
                                                 Stability = Stability.Unstable
                                             }
                               };
        Assert.True(TeamCity.GenerateNugetVersion(versionAndBranch).Contains("0.0.0-Unstable4000"));
    }


    void VerifyOutput(string versionString, VersionAndBranch version)
    {
        var tcVersion = TeamCity.GenerateBuildVersion(version);

        Assert.True(TeamCity.GenerateBuildVersion(version).Contains(versionString), string.Format("TeamCity string {0} did not match expected string {1}", tcVersion, versionString));
    }

}