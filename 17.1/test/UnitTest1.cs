using System.ComponentModel;

namespace test;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(Data))]
    public void Test1(string value, long number)
    {
        Assert.Equal(number, lib.Class.Function(value));
    }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { sample, 102 },
            new object[] { input, -1 },
        };    

static string sample = 
@"2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533";

static string input = 
@"441132215352334323215454261221632644656445213353552622323563332336541646635131673357444214232225511162441445635113614351245453321325543514424
312215453552151141123312226465513535442234626625432245665237412214265255271211437124272714461512255322222164516522235245245534445413541221433
443151214341313223414136533512615424112315121662431175772242275162553116674144511314266234544414242415455126361543514562663322113244543311351
332214452151354115414225115552132652315225662141412737534647165564417642465472123263413564732546146351125624511461356362213422241145552335335
111231515424424415511526233341223214634136446246727762247242464222611262622274375355571732753224665336232665342252311451465611114122124541123
222225252444221146565351533441332342414266522175136523742733276476524466411723627531552427377723114711646431533356251526631562142235231332213
323342345314322115664521625442132322465333737135734316666533727547452174462247456315755746534352124716377231214411334325262354511254334124135
533112512521213133314236121416355315432552125712563416375132723612753244752652255465377557637522755746576125256632323116154231135333151554122
432553154452551656365631256532445252731574314237724713136126257175112152563613516612435461514767441662521112161452222654553311336433231235345
441435214525453114522123225315166171224546624427247662224167577447613124525252741566672141773321664322455136576145361323551612524623211244213
455431413323425666553625262243653454712264651711352367453756666143744741365524413235622567467727322573757634246641553142452515516345541525113
311121535522631433331434151433575114444714627615633566636741547285868358287456768777554433514353215233713746512536156552154336416656551213343
343234532464655646233325365344366262267726535411625774162776846856754733628573586773735236723733355261352423616745515412245434365634232521521
541441244326541636225326444656436331473224744433543134658583385856875338322577532822457533436555725232563575472643524465425522363236423554411
431321252265452153351653542526721426154544235152534688582533836443487228624582778355367333624547261574165226332247645166455454166442364541411
542422221255332464663521111624461661426133266672538475344268726528647837756747263553835738635225467443765431514525241765552625155364446342422
114341642331122564554562271133711235346252546537748534826552288453824773264666845548564526574623624362326557462653161244241155223215113322113
545532464353451423366322476527215224241522568224427848772435827254845875482545348754876638254285276776214437631641315523213236234162323325351
135452614152566153223744554722511266165372885467443288433378762826684877435846536375576452662282785364417413641573274166715146566244624124341
312155623456555353167123757145263313244662337882837232584374744684674542346723372324364337655352742745654513555357175771572464324641622333225
253565645116252312225262734624465456372678338763887278453366646368786736468377456583326375748233867383868223732446462251537312166442131634312
242222345243542212557356336344356525237727643365447883264376653765827324452236348472748662426588432542843254133522415511567535665165245232544
443255162444332161322553757645277723377428527647453882785623543362722426244266726647585266785262344665438843271327741345412134616615221332266
425644225565131414324614367732655162357457226346425783337754648444973943667658446783522767436448286383664252361754735251671414553422133263631
346554533425325443551661216572632682273753567246568483354585653354939333399674755667654834685466738253848357246647634412654724332266155152455
336242145552246331717472431114578533783477524762637386267974666738354556877573659679339687485225876265388628254521375235675617115341544435625
663252661156311345721547556242553353786483885344725874854485335953869533649888877475938684743476656327268256362682721516664416312564211535335
534652616152654665232517554415872782875686356587388585583337733469664644648658659844463488583264626858723765262426167117612423326142151443616
411624561434361526343771514363477478547425784755295796953366499353339946798576465398557685635357328523778544553582873162677735313344356214364
445554235636623656436636541653643232726523436245943898587443847464657449844378896539433965658563864322846328323428273643771322151776546251252
366332622221217674747267775284472445578287465685999638548488743778368649736399655399564339338453775534767334775353236312522376225525113144556
235156511126472366333665415222375564322752485339564746578434838794638939476997677886668557777455877857734348273227867423467423474752455145322
521645134555261423735172326456443585338453999367974663785557559389458675678758399786434673445889697464857873652583628317417777163436365314521
132114622511561521354525626367554345273743359747993579596545557478699958794458549768894786557563896635726648858332478323633757363547511552265
641422647745763517476722554527737383446363883439667665448333676668974553339946354485368585689886687368635682838552848484153252144227725221134
555453237317324554143285383264624882368749693583676369445965857888865884986699789399997788356697639679832442678872624245472776542226256633664
152635466163266312636787347446266755583587688435675574957369784794967977974547764983348763775496334837946432488538844785524553544656261564425
243511451223447226265862665377372838877689559445538499895654466947568555545455787795895663794399353334857864838533268784235417473512453723152
521465352136542427525877284553323234875638333875859466584485697757996975866549857697445577334775354667589684835758388882224346623267352651434
661644521447271451717673538837662643464586844685844596748974994756478445596554948656646943846466398993798893234444883553652243676626142475411
642134664164337256168765265786637637949669676889389445769995549796957585699856796989899954937894565855955648727336842877737533347141676226444
242131464333271121234854527526434847757584966534575588975995944874648466795699875588874868599978388853879749645258885672335453664162316526264
344337645272371332543357345737223384786659894579898869674547876888684545479958599675864988494959689778947944868557473484575235764531517563234
246152264442155446656858557345736796793898738894999749658894549655747445459566865758695596984873647474656998499675337758783332422677343315351
152162376617366318342534823632673456473693968934846674868766887975746859695589558665799899996868457959769676598565474256272767462166437752121
213221561431577432544537225388354657654975576365574854478495749955954445466949969498766684788774694633966585569873222463852436551271344257544
452522555564137443743644373862333733784949849957789447994567899969966847768979659946974877597894479398734399488998743235363724214421137267362
166664253447732352324377842734594573679559797556964495879659476546996985777755475779985994697957754986484459895497468366285533313334566422376
333161663423335242333636542484867366875978345976597888546778956585885875699866785868895744856966868657557797556759244733337744584321657672447
132434116132626883787742725256833758959773465848754598765759989885665965875786666777467878989867766445954634878745564484245285547624255577575
621221772266627265485385467443348769887453794577647465766998855798666678987666778596589654955655575766599639449463685246252337663174373573661
373117531661426875367455687976548878395695657584569658546489957968956576895698859885974685884778689477655367457564787368586882878674541311331
237753716476658823648325522353989656544698974458844994475865988989876966686885565899978784658565697884633667675977563826362432546531317746554
137575267444274456643655325447877553547359784697677595469857667678877877787685959575555976488785659959669484339758639365855856624831156655115
471155645426466483835822489663944685393456469486458976799888599896559887857665997898577788756566754877545874735595959862385386853577516742765
224336246436358237428456684394853845666975586474868969677856866756955697599877699989658678895579676999498754346695385522427467758314275573376
476234633113285387627658744388456755438456464587449456599665679857667659968998668855878897998964575659977984577655675423758836773642713757362
674552474613575547227438464878395739699799446766687578755595678689767977768875667777668785689555766947658796557545336477626624878534253122641
261617276157734485622546364996448964776449688974566789586857995657685969796689869899869966975597894685559534634789355456674552347885225146134
442777416665864524878744964396939599464494849499858689976595667577956899975587765986967598687459469646546993784446654875664844832841156272155
622771446363772623847726769593366878859987744986568679869797889876987899667969857855958795859549494644859643963678793478738262237562266172437
211454341762354335787858378399337633658489557478797668997697859598866688886976789789859965677966966556996693873383496482432783573744511376514
751556737627475844836858777466934337847877849568859695876776588697777978967787697778656887575676859547677897673775868886638466848324761623777
377354132668432838654245968867799665976946559698898698556997959989678768979888689798685889999886475957598778939983699798628675667235276235337
274461211658662748736846375658554664777986459774867999679958977878677666676776986876987565675985899476779478874853583656653853235472623156143
175176442537338365485757955983393796967568584595567995796768677676879767978669676978765679855988689687586496884435963743225446323368526134313
327477746736537356268737497865339637694945874578786568696685688786676799686678787799675787766966644757898548768635896433524233325873323531766
156164616336284368674837853653357745759479794786968587697878697678978778788866787895567757887669484545559967659667544795424624678628431214145
554172253546762372852384849959746735777578568676765796777599887997989987686877678795889996776587958745549499346578486345345253344552344467135
115361712464362778387457749658843586979486756787687786789898686979877987987898789986779799765966877996759689957864443593264253773473475136727
163622266213475473332524336653854587869675584649988577985767778889898969668789866888766967579688486758786998336985783663622272524647564333721
413565362427484666565324765337933789855846766679879967899879968999788898786867868989589975685859984549559577935399659748544854364463257241131
734267363348737634854254669684633855555786999959776757655576897679989779869986778769765568799795475995997795998455643464264468544366626274765
453512137778447488874584576756874779865686459587777977656678987988996989778767888669998985967965847555566869546699666586666783836378336143571
135637421725653628777527599766638379689948545649859887759765787799877968878977989797856775559798886854679464436953479685738433884572453533173
444663164366378872874779754498969449544875795549986859669556778696697979987687996675859996676665796494968586437567394699276337452663465261343
127116134543862722762866478944339589787868978597989885999959897778996669869867877877886956585676668944485875483578379346226865867634343515572
474661117247376223643829958878464978475946886844898755678755778668679867798787867687858975585969544976755448773434969888465833476635267716535
163512764415232867848659599935796775465995586466695666797799567789779767897999677596579658565664469499896578563989978387574724375574112665324
236766431457362345677253773394589757897897648855997856678985787688898888778989997996655579865659869444784779544457366755838877565574442572524
444736225332287633282543775839354399488579598876987969987778687666987967778998775895555595558679464686584467853783394962588437573676355252457
441276346662462765544436993663854697569658776444578677579575969896798787967876888675689776877664646784449465979895459892626434773265231733142
433613241731478734756528563579679937899755657477475796565957957897896999689978787699885775797875475678578799865685476783628885874565472513546
764517777643732467263334954854754743364967566676876665786669788899587768795587796699599875868469756895675474896747435947256482534277137252246
146254313627228632567848696753665754749555769794456577887889955959779867877598575769896666878657595499947796579476496575384527557581517611757
454136634466568572872758375783839333835656745485847576777979679959667978866787979668988986879468654784495695934843637924362684665764253341745
645163375143873683853336668777494638445657449756966975976598878988665757579686565589678979885569964999878483789638443734658576854432555141655
757343244766772478344353365737585587663887975784597797595678966867595868978686965579775996497754784795496689436846898463652572663676413377214
221757314717222538374544257565476753394876896586669758977786858995587985795888655787759967765965454677779943484578349667636878442813611666726
526167143646362624588564423893748596767365478447959954497859675558575696787968685557768867958785764495639755954969359266526226486867736154333
555434774144157287264485874564569489568345977665789487898576586766666696789688999695766757676786757574686737875669467846265432776743635111112
627477455535138736258422366365737466674957486449885945899889559667759879667598879995759584968767668464466539887695964226548236547625111241236
354242336113743633488742554553668785387548469789655988679688658698566959589555759775858966874795797484544554463569623365763257565643512134757
354174617737214455645265358839646785358894855888566745895594586677687855855987975987965789555946579688485954788758482336565847474541576757642
433223277146325568636627656844473348577364669586457875485789894889979675769957779658687769878698657443478675546554732425647744442713553365665
123651713244737242287542484239539536999657387877894874495674766579677758587558849945874848889956846984673457733895372824734635423474162726662
261764134147453183452427246464685474894645335668446788979546477567586697495966569558595979745585783898358565349382368342537623532135351135761
434525335326732444343336353336336489554547539888748877596999668596575556845654986487899557765699749769774648583486834636773347315156534636421
423325134413547743245784758684494333799589579798866875448756955846458587678794558888499696589569989779558933766968756834776654253744733264632
163555331742665135373354723432235485834536567976797888644486946855667797488896856969677574577965588996465883346474276364838642117357571513526
226554417547665137354787883746525577948875759966468848565667888748485545947756897689657894795858463487737634684384444427555275414133325545434
214134141512441461455423767736724456565564858446965487984666847744966465875585866877877495486485566677376998387527264626285461113522612576563
364443116251377276428346386884785393848653473573565844645447579559497747689549994946744747699849376398587639983435458276456244736175344342433
221614177364461214633437227664885533599599375643738966657779599865799766957644895566789754364396474643445655246678448722727113275256531666442
121625462722541367136744733727287474955566746485953969746794945569557685489567499456459867443839373594335998736663728354478265261662113262455
634521356234133322565625835554682477386896335835368886679368655679455886694978769575735666655465669338484324834657235448345133611623453463326
366331262414426324275158752437682856376756874799336644897686974464894796765769897473666544743979784598878543534876353656535225644143774742361
235115247752344735316164342837876825585787665438387435367353583999679668795966587458949559744693836833885782686674552387562573163772115362152
256666526741567376721725565874322482382785943764654645933753693443865838774395365556477893654375979475428538636864848683626432156455457121634
142464523154325446566325826787478764662659543335569774559588877767636839996344654345355745873397768869443834284586822647153612121344621441545
543644532466122713622115474242267387843564944893788594336883663878995568753499439487884465347744659757753585275567452555326334331266534511126
214564435166152722576664753628823644545837268686737754393346794789338367567865864557755649668856889744457452572327435415444631346661336413334
114315451412621344516724615764433365732453824736754358355358568779955358455833358644974773643975846786384586638824585576561166176537323546521
362135554163743327152433617236372373235684436573749449375536797563786549594454563669586989645776834837463424827648255242167764436716613564253
523545634324167653432352254623863533357285272338674795673943863656839375455849376773568576943952464367748747677385547335777564435562542252666
166335123634666377322453322535782485472646382648742759935956989633777498957333975476339339636433484538734638434387523736255326212544626351522
254152361554213514221665631636524482768855462645667846674354468875848564799793754555379856834284846845568765665726755725223646655663241543351
325543113165156544621447547362438332654275884226726674698836776556996699794479376596635463864767836363543353542144423152644321566441563233222
343236324554445566725672625333367573872687263462887384552747383547964739743534878894774755634672852775852424245712237324445255324311622133225
423314112245216245365476275222671686573422687387645545237366478974344995985779696433272357535223562788254788434657313161254156223552463346521
146364144611121132131223356722341522435662567832865527623538447224885463365422662525737238544268565386653266113366414154327467441211225525646
341466334314323666361723641443167331176862844866552388883577244268278636787466375383773447727378325876725354571574213766321654536215362133332
551542123634564263146261366764472472423244685272246266636456437666457286685848855747858837378466788883526273547677514661222662335361334215641
245452515434432244417452627563514161456223237224322283426865635743324887556525546733446283235236242532664415767677477766774231364556566163532
443146236552466336566627775241367173416622453876288335348342878684624636235383767875332652466274346842457657537221232531513653341133426452211
534256244245666636554365453233356632662346462662736585752687785478276677774587572287385367563223877825645262465325242534421253442551534446432
251241634626225335643155645266472441232441676425826845822652228573433362346534582424263345587488622537415373132546772122623153361115614631312
333455156651356526242256217712552265416277264752883853436362877434722524687478387558574234763281124566344474371552655635365626563435436352525
544354416221536441621121531163756554625115627355775567445883878838375558847365224323642224722156415161411354631312124612264155362526521442435
221512312346264526213533521345741566521315135747365514837576332654778722788754528422855838453516712223774471157416553456653512145431613512132
332234524143462123446515126676137416736627145431353355457338657242866352683374533656747151672163651764635424431462634451125656236124522323342
224142451153423612536543614253551735626225435273356654215557633448758342637377265763325311515665741251561242223672145214564326531113441454444
232534241435165341141364554254566452517144433733577366547173465726465753251564475231731464627155622171655324616635224125614233343354414123211
233345314235324512136311233645443476726547342527254113271457241453636277261631775656574154342327527536665646761566134225136345452233544543133
113411551312432131365261244545452356745237743732261226755215521332771752351142212751473561632137125435715662714513235123424316462522511111144
132554544312245432661363324661562334711537526336731714544225126146371635551465122416555432465372635461535565262433625356653635351353424122223
435155211555454545245511653545562146261333267637473337152623621476166136676165144734113454363231563242122443116442656234322536631513111244345
154155552414124323555365552252541562516216366422645777772262211316321711434756764634147364447572562373733435326416353643123455342235415245441
415135134541121342242366253353225443523566331637521151656477522735321376546446222264527313177726354155314311452531622362215513132245242145253
332514234133341124331523452633243233166641421154257763614372532122621473575677355665336364235771372415652212313643211421151235455111335354142
324213413453541522242553321315411652261341523541756743427157265273647534624636244612711413736115421235424452166545213562252524433443341555152";

}
