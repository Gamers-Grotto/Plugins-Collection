# Package Creator

## Workflow Post Package Creation
1. Upload the package to GitHub.
2. Import the package via the Package Manager.
3. By default, the package is stored in Library/PackageCache, not under Packages. While it's in the cache, any changes made are temporary and can be overridden.
4. This works fine for most users.
5. To edit the package permanently, move it from Library/PackageCache to /Packages/. This makes it part of the project rather than a cached package. You can easily browse to the folder in Unity by right-clicking on it and selecting "Show in Explorer" (or equivalent).
6. Now you can edit the package with full control.