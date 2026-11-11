using System;

namespace Pike85
{
	/// <summary>
	/// Summary description for ExternalHelper.
	/// </summary>
	public class ExternalHelper
	{
    public static int dataItemCounter = 0;
    private static string itemId = string.Empty;
    private static string pageStrId = string.Empty;
    public static int DataItemCounter(int maxNumber, string dataItemID, string pageId)
    {
      if (maxNumber == dataItemCounter || dataItemID != itemId || (pageStrId != pageId))
      {
        itemId = dataItemID;
        pageStrId = pageId;
        return dataItemCounter = 1;
      }

      return dataItemCounter += 1;
    }

    public static string DataItemIsEmpty(string dataitem, string returnValue)
    {
      if (dataitem != string.Empty)
      {
        return returnValue;
      }
      return string.Empty;
    }

    public static string BuildPictureUrl(string picture)
    {
      return "javascript:PopupBilled('" + picture + ".jpg')";
    }

	}
}
