# Branching Strategy

This document acts a quick-start guide for the GitHub Flow branching strategy. For more in-depth informaton on GitHub Flow, [please see GitHub's documentation.](https://docs.github.com/en/get-started/quickstart/github-flow).

## Branching

Our primary branch is `main`, which will contain all of the latest changes in the repository. All branches should be created from `main`. When creating a new branch in the repository, we adhere to the following conventions:

- For single issue branches: `{issue-label}/{issue-id}-{branch-title}` 
- For multi-ssue branches: `{issue-label}/{issue-id-1}-{issue-id-n}-{branch-title}` 

Our options for `{issue-label}` are as follows:

- `bugfix` for branches that are fixing a bug.
- `feature` for branches that implement a new feature or functionality.
- `documentation` for updating the repository's documentation.

## Creating Pull Requests

When creating a pull request, there are a few suggested standards to adhere to: 

- In the body of the pull request list changes, removed/renamed files, etc. and try to be as complete as possible.
- After listing the changes, use [closing keywords](https://docs.github.com/en/get-started/writing-on-github/working-with-advanced-formatting/using-keywords-in-issues-and-pull-requests) to link the issue that the pull request addresses. This will ensure the pull request closes the issue once it is merged into `main`.

In the repository, the `main` branch is protected by a number of policies. In order for the pull request to be marked as ready to merge, the pull request must satisfy the following criteria:

- The pull request must have a minimum number of approvals from reviewer (typically two, though smaller teams may require less).
- Pull requests must be linked to an issue.
- Any comments on the pull request must be resolved.
- Approvals are reset when pushing additional commits to the source branch.

Note that the above requirements may vary according to the specific policy settings for the `main` branch.

## Merging Pull Requests

Once the pull request has satisfied all criteria according to the branch policies on `main`, the source branch can then be merged. When merging, we always use squash commits, which can be performed by using the dropdown on the merge button. Squash commits combine all commits on the source branch into one commit, and prevent the git history on `main` from ballooning in size.

After performing a squash commit, we should delete the source branch. This will keep our branches in the repository clean, and ensure that we don't keep any stale branches around. Stale branches can make navigating a repository a bit of a headache, and adds to the overall size of a repository. If the merge ever needs to be reverted, we can always restore the branch from the original pull request.
