using System.Collections.Generic;
using RepairBusinessLogic.OfficePackage.HelperEnums;
using RepairBusinessLogic.OfficePackage.HelperModels;
using RepairContracts.ViewModels;

namespace RepairBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            if (info.DocumentType == WordDocumentType.Text)
            {
                CreateWord(info);
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Center
                    }
                });
                string tab = ":\t";
                foreach (var repair in info.Repairs)
                {
                    CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> {(repair.RepairName, new WordTextProperties { Bold = true, Size = "24", }),
                        (", Цена: " + repair.Price, new WordTextProperties {Bold = false, Size = "24", }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationType = WordJustificationType.Both
                        }
                    });
                }
                SaveWord(info);
            }
            else 
            {
                CreateWord(info);

                CreateTable(new WordTable
                {
                    Header = info.Title,
                    Rows = info.WareHouses,
                    TableProperties = new WordTableProperties
                    {
                        TextSize = "24",
                        BorderSize = "6",
                        BorderType = WordBorderType.Single
                    }
                });
                SaveWord(info);
            }
        }
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);
        protected abstract void CreateTable(WordTable table);
    }
}
