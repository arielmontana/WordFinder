using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordFinder.Config;
using WordFinder.Utils;

namespace WordFinder
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //This code use a test matrix
            var testMatrix = GetTestMatrix();
            var testWordStream = GetTestsWordStream();
            WordFinder wordFinder = new WordFinder(testMatrix);
            var words = wordFinder.Find(testWordStream);
            //this printer is not generic, the purpose is helps to identify visually the test matrix words
            PrintTestMatrix();

        }

        #region For Testing 
        private static void PrintTestMatrix()
        {
            var arrayMatrix = GetTestMatrix(false);
            foreach (var row in arrayMatrix)
            {
                WriteColor(row, ConsoleColor.Red);
            }

            Console.Read();
        }

        static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.BackgroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }
                Console.Write(piece);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        private static IEnumerable<string> GetTestsWordStream() =>
            new List<string>() { "admin", "hello", "down", "left", "drawing", "almost", "trouble", 
                                "choice", "goal", "watch", "low", "commit" };
        private static IEnumerable<string> GetTestMatrix(bool removeIndicators = true)
        {
            string pattern = @"[\[\]']+";
            Regex regex = new Regex(pattern);
            var stringMatrix =
                "lao[hello]vlyvwsxihnwtxfnspwuclxcdetqudvccx[down]jynetyltkcdgocibyuq," +
                "ñkwuwyeenohkkeaqzqkrldnkghhnntrhqñmqulbñnzzññnpcukuadrjepfhikkkm," +
                "tmñlbohjoxdwromkxlvrjwryatjwmwvjgqntukmoglhnyarjoqfshnzpnjdaydp[l]," +
                "ñjttligrzerixsturbñkvumkpzezdivñoldkqivbrtabbbijdyzuvtrmpcwewzh[o]," +
                "wmgicwcxfluncbikooxbtlñmknhekxlfjtxiustuppjbpsguhnadcbzdqnriiqg[w]," +
                "iwbvsioñvfwvgokzfpppawbnnkatefrcbaaoxrhxfizovbdqacvujemfoi[choice]," +
                "nnyiouhbmdlofkeacnudpmphñsiiypkykbbhxbeahmqttjcqptqtbyjtpjnofrvo," +
                "fxwbpvtanoxophxjklxqjpelalhhjñijuahdyisrpmgcfysviyñzqqvimetxhflr," +
                "hnisndulytpcuyulerdsclwwqknhñddthwqedvññmhlpyzkyrgvgzrpwjeuvjgpo," +
                "oñqajbbxjznszqlshiaapniauñrtlxzloydenqbxncklvocgcvupihmuyjyñcafd," +
                "vwtgzyjzvghvpxyedkiwbv[a]fby[w]mgñfpzkslbsvpeketyqbescrjpalsohxtvpma," +
                "kpyxgwehlllkzadpkmwoñs[d]sto[a]ñunhqwshuvzfygvyisajanxdxieykqzwlcjfp," +
                "ngz[goal]zqbmncrlyftpccb[m]ñow[t]vnopjbeynchedsqkvfpgzefqiign[d]oepxpafu," +
                "vermqoqifcflibrjikbrat[i]kñq[c]kzglehkceiggjrsvwamzwqhajwww[o]jaqsqabs," +
                "twqrgtuwlsaqkcuknmlyuz[n]hañ[h]igllkvgzicxdgyffwñwsqobwgkni[w]lcklnfks," +
                "jñwwfbibxsiñamwñ[admin]tcqfpsglmmñfpxboncsgxfmrrdjgpcmfev[n]fykibuje," +
                "ncdhñrjdxtshirgfcsñdrbtmiojvnkehtexjsvlqmcadvuoawvsuñtbebkrrcgjg," +
                "egwnpfzaqjjtjjgsbrjlcmswmabwmhzmññkhlgzjmnpdldltesthsñymgttjwvhc," +
                "pyorrañlsriñidaiblmglmsiqulgjfjyhtoxouglyosojqqimtodñokwyztpiaug," +
                "daiivmzxtapnxrtwvnjkydlblritjvqnfgyjbtiziznhñctatmfgeovsiypwxyhq," +
                "vakmdñuxcusjwgbqasqjebjdytvweucloxluchjqdbumvcelgzpyjaqnkzgclqma," +
                "anrñzhlcsyvypaojdjxxvuhzqdvtzxfiñtñiepzlxojuohnslmbbhvjyzlpoelvñ," +
                "geoeywgbfszefddjvbgpwtldicmsdagmobbvqgfiñsuvcñnagyaciqiflfopdgqy," +
                "giv[admin]ruxrjtñiefyrwxqmpdekiwauojoykyyfmfjodfvetartlybckcypzqan," +
                "vvsyiqyzmzkhubcatjaxabjafhascyñxovbcvye[commit]skñoizasifnglmtjyqg," +
                "flrtsxñxqegbjjkrzxnrhxzjhpdgdmgoghxwjsñdevuwrdovuvduvgpaecfwnany," +
                "kouehbzccgewukwsaebbemrlpkzhljwboyzhñjgnwlmsew[trouble]pfrpchoulkm," +
                "wzodhykqhxmrszmaiyhwaosmblduklftsjapganqñqqlmspvohffvivaavupñizo," +
                "lscroñzejccrasbñayvysqdgsjdypwykustsgwsnytfzmbsvmugcwjmniorydthg," +
                "lpasbzwybsrewnkjagñjogdzzelxncxfsvtelvdñcwñjnbduajvhnpbxftmqires," +
                "ffumyqxvszllñgqsjpwzjlvnprkcfzdkonapcaulqdnijekgoqqvalyncaozyñkm," +
                "tptytkbnebyrgxeawghaacviggueñfvntmihkañzjapcsoov[a]rubveiknemgiprr," +
                "eqniojplalsgmikkrtodnkiwqñyhalmbqvgquhhbxjñmiwto[l]pjovhfnbkkudadi," +
                "kijumhdehyxoognncgdupyckmiepgppxynrhdnabydhbaqfv[m]atnsgudyrawwgrj," +
                "rjuiyciyvofkubowbtyovxllhbefwxleqsbadgkqeiqkimxj[o]sñfmnynqctpqvtq," +
                "tmtshmndadhatvnxñswczlwñhctfnsbgshhsvjqztueckgpy[s]lvwpgpbizexlajz," +
                "szghnuxupcwznwufñyupyjfkzepymhlfxbkzclfjruswmtñv[t]lpugcodnoabxrbb," +
                "luefñvwmerwjbqyjnotkjrñezfgdwesmlgtrcjhimbudmnequlevzktmasycimjs," +
                "dqopzawaeqbpdmgtiadspftfalbdytslufboiuoeaññllppzlbrexsuqpchhqlue," +
                "iñqdkdhfslukbneñulpuhguddqzfagimdñvurotqesyaszihwnosyccumhtmxtxf," +
                "bzgkcoykrjibncvclcgjigtetbdobmcñmxlsupnqkczmnnswpcflkohñzvktecby," +
                "niilvañjjsyzzkzbzrrzlwpkpmrylfattfsxdfnuluuobgsedlxdcrhjkqñcchpw," +
                "xynjurnxqijutbcngosapkvqcgfkdlvxqwvayñbndzecqhjieiolpoñgwozifvmy," +
                "expythlhzpjymsoppyñodaubixchmaagzqfcmcdtwxkrybdñeliqqbtxuhjpxwñr," +
                "vrñgevfnñlyjwvanhwtlciqoaayngotdlbvciarivwwjñhrebwjsnazgtrywqmzr," +
                "orñsqxzweiyeatnbnedxsjduibjvñldpxqsiwderdobltunqeñwaonlygldihwbm," +
                "cabtwxcseñxalughlkshvsdznuesñdzgigkndpajnaergqñkpuogbyvcovavmcxt," +
                "klxdmwnccnñiemñzñvnhnxcebqbjxpevdeoyjzxquwreandceorzdfnfbmunñxwg," +
                "lxtoiihqofjyqwhhrwcqxlpkdppxdvpseviicgpzdbhtwzdjhzotñhxracfllcgm," +
                "jnñhrx[d]avgñwpgwfajwzpxpqjastxgtñofgfiuivdmltcñdobyemdfkfqpqkwdvi," +
                "tbdtkp[r]ttowfoiñpxlsvadcnthlwqqtjvhbgsñkaylcjgsuavnslwfxjrjsvyuvv," +
                "hcazao[a]ukpjumatfdtwrbfjcmhdogvxiznrjnuñqjjejhhybrjñsrwgumqgnfggi," +
                "tñradl[w]zzaktsñxbuppawjvzqqogtewvoqgmzthqfnlootmvcedjjbhdmldfpñyñ," +
                "hxuadv[i]asndrzepxmxitfghcwthjutlhpkaokoqccbjgxefzgawjjnltjlwdhdyr," +
                "ouiwtg[n]yzkxptylhavnptqwsyzqlhñrvldbcqvfnzddugxigfozaftuqqiqnixke," +
                "uxjpak[g]jsjjgteukeylhljjcdebylxumrcessjwxvkfdddpwowxrrptehzfobrpñ," +
                "qhksukñugetxyrshxbiswnuctemwaownkogoywñecpfeszbafaxjbñewfarjqknk," +
                "zgcfxbt[a]wjzajcnwfkslgeoebbqxxglvñwmmrñarwvxxxooqyffsnrnnlqvodocj," +
                "wffjtxk[d]theakzffsrlulbpcruslf[left]ztdcbyañjmdcgtljcdqkhyojwkrxrqf," +
                "lmdzvvl[m]dyvifsesbuvwcvstxnzkzwplzvlnjehzlwzbwzwwxropñbqimcubotgz," +
                "dbdakcp[i]oeyokpqkvfaaspqxvuzdydljsrmrmohirbrqlmufwknhyscgdyyivfbf," +
                "holxrqz[n]makrsebsnyhjwdsjyhgkqfftfwwpcbxngjgllhdnpibznojwouqddisz," +
                "keoaejftnvgqaqkwnncwyzkhstacncqjzotvhferoysmvyhwzajñjvjdñaadbvzd," +
                "pkephiabdaszqenfxvgmvswzjazwtjmrihyhvzllbfzñjwpogqxrmktphyz[hello]";

            var result = (removeIndicators) ? Regex.Replace(stringMatrix, pattern, string.Empty) : stringMatrix;
            return result.Split(',').ToList();
        }

        /* WORDS INTO TEST MATRIX.
         * 1) admin(4)   [horizontal(16, 17)-(16,22)] - [horizontal 24,4)-(24,9)] - [vertical - (11,23)-(15,23)]- [vertical - (58,8)-(62,8)]
         * 2) hello(2)   [horizontal(1, 3)-(1,8)] - [horizontal(64, 60)-(64,64)]
         * 3) down(2)    [horizontal(1, 42)-(1,46)] - [vertical - (13,56)-(17,56)]
         * 4) left(1)    [horizontal(59, 30)-(59,34)]
         * 5) drawing(1) [vertical - (50,7)-(57,7)]
         * 6) almost(1)  [vertical - (32,49)-(37,49)]
         * 7) trouble(1) [horizontal(27, 47)-(27,54)]
         * 8) choice(1)  [horizontal(6, 59)-(6,65)]
         * 9) goal(1)    [horizontal(13, 4)-(13,8)]
         * 10) watch(1)  [vertical - (11,27)-(16,27)]
         * 11) low(1)    [vertical - (3,64)-(5,64)]
         * 12) commit(1) [horizontal(25, 40)-(25,46)]
*/ 

        #endregion
    }
}