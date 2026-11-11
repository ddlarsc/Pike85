// <copyright file="SkabelonPage.cs" company="Pike85">
//     Copyright (c) Pike85.  All rights reserved.
// </copyright>
// <summary>
// Class Name: SkabelonPage
// </summary>

using System;

/// <summary>
/// SkabelonPage class used when going to design mode for page refering to nested master pages.
/// </summary>
public class SkabelonPage : System.Web.UI.Page
{
    /// <summary>
    /// Local variable for runtime master page file.
    /// </summary>
    private string runtimeMasterPageFile;

    /// <summary>
    /// Gets or sets file for runtime master page.
    /// </summary>
    public string RuntimeMasterPageFile
    {
        get { return this.runtimeMasterPageFile; }
        set { this.runtimeMasterPageFile = value; }
    }

    /// <summary>
    /// Overrides the OnPreInit method in the Page class, and change the MasterPageFile to the runtime master page file (nested).
    /// </summary>
    /// <param name="e">Event data.</param>
    protected override void OnPreInit(EventArgs e)
    {
        if (this.runtimeMasterPageFile != null)
        {
            this.MasterPageFile = this.runtimeMasterPageFile;
        }

        base.OnPreInit(e);
    }
}