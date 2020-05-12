/*
 * Copyright © 2020 Michał Przybyś <michal@przybys.eu>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * Distributions of all or part of the Software intended to be used by the
 * recipients as they would use the unmodified Software, containing modifications
 * that substantially alter, remove, or disable functionality of the Software,
 * outside of the documented configuration mechanisms provided by the Software,
 * shall be modified such that the Original Author's bug reporting email addresses
 * and urls are either replaced with the contact information of the parties
 * responsible for the changes, or removed entirely.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using CodinGame;
using Xunit;

namespace RockPaperScissorsLizardSpock
{
    /// <summary>
    /// Tests for the solution.
    /// </summary>
    public class UnitTest : UnitTestBase
    {
        /// <summary>
        /// Gets the test data.
        /// </summary>
        public static object[][] TestData { get; } = new object[][]
        {
            new object[] { "8\n4 R\n1 P\n8 P\n3 R\n7 C\n5 S\n6 L\n2 L", "2\n6 5 1" },
            new object[] { "2\n1 S\n2 S", "1\n2" },
            new object[] { "32\n28 R\n3 R\n13 L\n6 P\n32 C\n5 R\n11 S\n27 S\n22 L\n31 R\n30 R\n10 P\n18 R\n23 R\n8 R\n20 S\n7 P\n19 P\n26 P\n4 R\n16 C\n21 P\n1 C\n14 C\n29 R\n9 P\n25 C\n24 P\n15 R\n2 L\n12 L\n17 S", "10\n30 31 20 11 15" },
            new object[] { "128\n35 R\n66 R\n68 R\n81 R\n27 R\n88 R\n74 R\n125 R\n116 R\n9 R\n115 R\n4 R\n52 R\n111 R\n103 R\n77 R\n114 R\n71 R\n113 R\n100 R\n112 R\n3 R\n85 R\n57 R\n13 R\n60 R\n47 R\n31 R\n122 R\n50 R\n44 R\n106 R\n86 R\n65 R\n22 R\n37 R\n26 R\n43 R\n55 R\n42 R\n23 R\n45 R\n89 R\n91 R\n28 R\n63 R\n18 R\n67 R\n34 R\n127 R\n107 R\n41 R\n36 R\n61 R\n97 R\n87 R\n118 R\n110 R\n96 R\n40 R\n14 R\n102 R\n84 R\n126 R\n117 R\n83 R\n101 R\n80 R\n58 R\n82 R\n119 R\n72 R\n51 R\n21 R\n33 R\n8 R\n1 R\n7 R\n92 R\n25 R\n16 R\n30 R\n79 R\n46 R\n94 R\n120 R\n59 R\n121 R\n108 R\n69 R\n73 R\n124 R\n12 R\n93 R\n78 R\n5 R\n29 R\n70 R\n109 R\n48 R\n64 R\n76 R\n38 R\n104 R\n75 R\n128 P\n20 R\n2 R\n95 R\n62 R\n10 R\n56 R\n99 R\n39 R\n105 R\n19 R\n15 R\n17 R\n54 R\n90 R\n6 R\n98 R\n123 R\n49 R\n32 R\n11 R\n53 R\n24 R", "128\n75 2 10 29 6 1 3" },
            new object[] { "1024\n292 C\n827 R\n945 S\n974 R\n440 S\n685 P\n266 L\n409 S\n153 S\n202 C\n843 P\n318 P\n888 S\n544 C\n724 S\n900 C\n438 P\n573 L\n879 S\n646 S\n204 R\n190 L\n661 P\n793 P\n39 L\n370 P\n320 L\n916 R\n854 C\n504 P\n653 L\n160 S\n756 R\n246 L\n878 R\n375 L\n689 P\n549 S\n548 R\n759 L\n406 C\n87 R\n308 C\n887 C\n157 L\n37 R\n556 P\n872 R\n502 S\n286 C\n749 P\n464 S\n65 P\n563 R\n902 C\n50 R\n103 P\n909 L\n384 C\n648 L\n706 S\n314 C\n560 C\n961 P\n182 L\n847 P\n621 R\n522 P\n16 C\n710 L\n122 L\n956 R\n399 R\n253 P\n487 R\n398 S\n101 R\n545 L\n952 S\n84 L\n842 R\n512 C\n555 P\n848 C\n596 C\n523 L\n475 R\n572 L\n726 C\n599 C\n508 C\n731 S\n221 S\n47 S\n881 C\n460 S\n278 S\n579 R\n750 C\n937 P\n620 P\n220 L\n162 R\n391 P\n982 S\n671 S\n1002 C\n225 S\n606 S\n10 C\n268 S\n855 L\n115 L\n338 R\n766 C\n932 S\n611 P\n567 C\n612 C\n217 S\n761 C\n853 P\n807 R\n403 C\n426 S\n664 S\n216 S\n446 L\n297 R\n64 S\n619 R\n530 S\n55 L\n993 P\n763 R\n453 R\n339 C\n951 P\n335 R\n212 S\n330 C\n233 L\n626 P\n304 P\n819 L\n382 C\n383 S\n156 P\n442 S\n814 P\n108 L\n792 C\n769 C\n582 R\n119 S\n273 C\n22 C\n1021 C\n295 R\n752 C\n348 L\n499 L\n928 C\n569 P\n711 R\n904 P\n901 L\n363 P\n51 C\n942 L\n165 P\n649 S\n944 C\n255 L\n329 P\n343 S\n772 R\n840 L\n1011 P\n862 C\n275 C\n663 C\n145 C\n293 L\n913 S\n682 R\n128 S\n13 C\n392 P\n25 S\n81 P\n532 R\n922 P\n211 C\n432 C\n317 P\n241 S\n14 R\n667 L\n349 R\n828 P\n118 P\n851 C\n164 L\n795 C\n976 R\n323 S\n168 C\n1003 S\n372 C\n746 C\n62 C\n180 P\n15 S\n571 S\n179 S\n693 R\n68 R\n351 L\n837 S\n245 C\n985 P\n971 S\n265 P\n794 S\n997 C\n760 R\n332 C\n639 S\n581 S\n250 R\n835 R\n725 P\n402 L\n89 L\n491 C\n376 C\n218 C\n591 P\n607 R\n70 L\n550 P\n604 L\n271 C\n647 C\n374 R\n561 S\n445 C\n953 L\n421 S\n969 S\n457 L\n876 C\n131 P\n124 R\n903 C\n779 P\n381 S\n727 S\n334 P\n104 P\n346 P\n580 L\n281 L\n366 S\n177 R\n551 R\n743 L\n60 P\n472 R\n627 P\n608 S\n656 S\n748 R\n467 R\n315 S\n589 P\n636 R\n99 L\n480 P\n79 P\n123 S\n252 C\n49 C\n931 R\n773 S\n373 R\n40 R\n690 C\n714 L\n405 L\n361 R\n767 R\n188 R\n575 L\n670 C\n808 L\n189 S\n774 R\n657 S\n100 R\n207 C\n482 S\n359 P\n488 L\n435 P\n416 S\n260 S\n307 S\n74 R\n209 L\n198 P\n910 S\n477 C\n401 S\n437 L\n964 R\n93 C\n863 C\n184 S\n885 L\n44 C\n407 S\n389 R\n193 R\n783 L\n768 R\n183 C\n718 P\n987 S\n388 S\n415 R\n705 C\n524 P\n186 P\n26 S\n173 S\n592 P\n121 P\n197 P\n234 R\n640 R\n613 R\n802 C\n741 R\n893 L\n547 L\n187 R\n995 S\n677 R\n637 S\n111 C\n325 C\n978 S\n176 L\n408 P\n803 C\n495 C\n23 P\n898 L\n659 L\n428 P\n159 S\n448 L\n820 S\n678 R\n32 L\n662 L\n578 C\n1008 S\n316 R\n455 R\n132 L\n191 L\n924 P\n350 R\n371 S\n787 L\n397 L\n280 C\n683 P\n91 R\n362 R\n352 L\n697 R\n730 C\n810 S\n576 L\n175 S\n5 R\n341 L\n106 P\n856 S\n712 L\n462 R\n490 S\n466 S\n895 P\n546 R\n72 R\n883 S\n110 S\n638 C\n860 L\n503 L\n907 R\n1000 S\n38 L\n994 P\n213 P\n673 S\n412 P\n538 C\n424 R\n631 S\n540 R\n817 S\n303 R\n917 R\n770 P\n732 S\n911 L\n78 S\n364 C\n413 P\n215 R\n138 C\n276 P\n960 P\n877 R\n958 C\n797 R\n95 L\n866 S\n417 L\n852 S\n300 C\n740 C\n679 P\n327 P\n651 S\n815 L\n92 R\n430 R\n818 S\n471 C\n722 L\n645 L\n566 S\n200 L\n139 L\n418 L\n235 S\n800 P\n857 C\n875 S\n688 P\n918 R\n166 S\n584 P\n947 L\n533 P\n735 S\n850 S\n479 S\n658 R\n324 S\n747 L\n594 P\n137 S\n833 S\n791 R\n112 L\n564 R\n955 S\n949 S\n585 C\n762 S\n378 L\n541 S\n43 P\n751 C\n450 P\n136 L\n687 L\n980 C\n597 P\n675 L\n600 P\n507 R\n577 R\n463 P\n871 L\n310 S\n986 S\n444 C\n149 R\n674 S\n696 L\n88 S\n167 S\n439 R\n181 P\n617 S\n557 C\n821 C\n59 L\n920 S\n199 C\n655 P\n473 S\n798 L\n1004 L\n411 C\n498 L\n298 C\n57 C\n832 L\n836 P\n733 R\n201 R\n210 C\n713 S\n698 C\n754 L\n742 S\n242 C\n869 S\n738 L\n459 L\n42 C\n938 R\n441 C\n935 R\n701 L\n927 R\n142 R\n206 S\n358 L\n785 P\n299 P\n67 L\n972 R\n957 R\n859 S\n443 L\n380 C\n114 L\n279 S\n35 C\n386 L\n703 P\n744 C\n501 S\n254 S\n71 L\n635 S\n870 S\n169 P\n322 P\n813 R\n669 C\n929 P\n583 S\n868 S\n94 C\n992 L\n172 C\n219 C\n968 P\n102 R\n208 P\n586 C\n950 P\n311 C\n231 P\n146 R\n125 S\n559 S\n537 S\n625 R\n880 P\n143 P\n151 S\n642 S\n155 R\n753 L\n864 C\n565 R\n258 P\n410 C\n356 S\n54 L\n17 C\n534 C\n781 L\n602 R\n858 S\n943 L\n717 C\n130 C\n481 S\n520 S\n400 R\n536 R\n312 P\n782 R\n905 R\n500 C\n294 L\n82 S\n1009 L\n707 R\n686 P\n614 S\n830 C\n360 R\n623 L\n483 S\n270 L\n333 L\n873 S\n97 L\n148 P\n891 S\n709 C\n77 P\n347 S\n765 L\n849 C\n665 P\n170 P\n553 S\n434 R\n385 L\n1005 R\n896 S\n147 L\n799 L\n203 C\n83 C\n379 R\n465 C\n771 L\n531 L\n1 R\n882 L\n788 S\n539 S\n140 P\n489 L\n232 R\n666 S\n543 R\n890 P\n469 R\n915 L\n456 S\n127 R\n247 S\n486 S\n1022 R\n644 L\n622 R\n908 L\n76 S\n496 L\n603 L\n816 C\n511 R\n777 S\n259 R\n694 C\n63 S\n230 L\n977 P\n906 C\n1007 L\n367 R\n966 S\n641 R\n431 R\n287 S\n886 S\n519 P\n515 C\n680 P\n427 R\n345 R\n73 L\n8 L\n257 S\n716 R\n296 L\n892 S\n934 R\n236 S\n684 R\n624 L\n804 R\n129 R\n940 L\n897 R\n776 S\n11 S\n248 S\n272 C\n264 C\n289 C\n497 R\n66 C\n468 C\n973 R\n509 C\n990 P\n291 C\n429 L\n9 P\n568 P\n357 P\n154 S\n516 S\n163 S\n395 C\n723 L\n517 C\n660 R\n838 S\n542 L\n574 C\n404 R\n939 C\n959 R\n18 S\n433 R\n930 L\n844 P\n267 R\n834 R\n337 S\n719 R\n85 S\n925 P\n387 P\n492 S\n588 S\n846 R\n513 L\n630 S\n484 L\n33 P\n302 C\n775 S\n7 R\n282 R\n449 P\n98 R\n414 R\n824 C\n306 S\n227 C\n116 C\n369 L\n394 P\n458 R\n634 C\n29 S\n780 R\n899 S\n975 S\n141 R\n962 C\n436 P\n737 R\n158 P\n715 P\n422 P\n861 L\n721 C\n238 S\n708 R\n778 P\n789 S\n535 C\n28 S\n27 S\n377 S\n514 C\n454 L\n226 S\n494 S\n720 L\n53 L\n811 L\n590 R\n884 C\n526 S\n745 R\n790 L\n269 R\n704 S\n1018 L\n251 C\n518 S\n493 S\n305 P\n764 S\n889 C\n1013 C\n451 R\n598 S\n529 S\n19 P\n171 P\n552 L\n784 R\n319 S\n474 P\n506 C\n829 L\n998 P\n396 P\n239 C\n1014 P\n954 L\n801 P\n274 R\n921 R\n926 R\n61 P\n809 S\n700 C\n243 R\n979 C\n1024 C\n593 L\n2 S\n1006 C\n80 S\n652 C\n288 S\n633 R\n425 S\n867 P\n244 P\n452 R\n699 L\n178 L\n90 P\n144 L\n476 C\n970 S\n194 S\n3 L\n963 C\n205 S\n570 C\n353 R\n224 S\n672 R\n461 L\n510 L\n1020 S\n368 S\n629 R\n285 S\n1001 S\n991 P\n845 L\n150 L\n196 L\n999 P\n739 S\n105 S\n331 R\n214 R\n96 C\n20 P\n261 P\n527 R\n228 R\n628 L\n1012 S\n1023 L\n695 C\n865 R\n989 C\n610 P\n936 C\n681 P\n6 R\n676 P\n290 S\n525 L\n692 R\n988 C\n313 L\n152 L\n595 S\n393 L\n52 L\n1010 R\n758 S\n135 C\n419 S\n321 C\n355 R\n31 P\n354 L\n874 S\n195 L\n616 R\n4 R\n702 C\n161 P\n981 P\n222 P\n839 R\n796 P\n841 R\n691 P\n650 S\n109 P\n86 P\n554 P\n923 S\n21 S\n485 R\n237 P\n812 S\n806 R\n1015 P\n478 S\n256 S\n283 L\n736 S\n447 C\n826 R\n344 P\n946 R\n58 P\n668 P\n757 S\n56 S\n75 L\n823 R\n528 L\n632 L\n48 L\n894 L\n69 S\n609 R\n133 P\n284 R\n420 L\n229 C\n805 L\n262 R\n34 R\n45 L\n126 P\n423 S\n12 C\n263 R\n46 L\n249 S\n967 L\n587 S\n309 R\n941 S\n912 S\n729 C\n1019 C\n618 P\n120 R\n174 S\n24 C\n336 C\n919 P\n822 L\n521 L\n107 S\n558 S\n277 R\n948 C\n326 R\n786 C\n825 S\n914 R\n755 C\n301 S\n365 P\n390 S\n470 P\n342 S\n185 C\n505 S\n601 S\n615 C\n223 S\n728 S\n983 L\n984 C\n996 R\n134 P\n933 L\n36 P\n192 P\n654 L\n965 S\n605 P\n41 C\n340 S\n117 S\n328 P\n113 S\n30 C\n831 L\n1016 L\n240 L\n1017 P\n643 C\n562 L\n734 L", "34\n45 262 229 823 283 152 24 228 230 188" },
        };

        /// <summary>
        /// Tests the solution.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <param name="output">The expected output.</param>
        [Theory]
        [MemberData(nameof(TestData))]
        public void Test(string input, string output)
        {
            TestBase(input, output, Solution.Main);
        }
    }
}
